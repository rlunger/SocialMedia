using System;
using System.Collections.Generic;
using System.Linq;
using DotnetAssessmentSocialMedia.Data;
using DotnetAssessmentSocialMedia.Data.Entities;
using DotnetAssessmentSocialMedia.Data.Relationships;
using DotnetAssessmentSocialMedia.Dtos;
using DotnetAssessmentSocialMedia.Exception.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DotnetAssessmentSocialMedia.Services
{
    public class UserService : IUserService
    {
        private readonly SocialMediaContext _context;

        private readonly ILogger _logger;

        public User GetByUsernameOrDefault(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Credentials.Username == username);
        }

        public UserService(SocialMediaContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public User GetByUsername(string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.Credentials.Username == username);

            // If user doesn't exists or is deleted, throw UserNotFoundException
            if (user == null || user.Deleted)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            // Find all non-delete users
            var users = _context.Users.Where(u => !u.Deleted).ToList();
            if (users.Count <= 0)
            {
                throw new NotFoundCustomException("No users found", "No users found");
            }

            return users;
        }

        public User CreateUser(User user)
        {
            user.Joined = DateTime.Now;
            try
            {
                _context.Add(user);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.Message.Contains("unique constraint")) // hmm
                {
                    throw new UsernameTakenException();
                }
            }

            return user;
        }

        public User UpdateUser(string username, User newUser)
        {
            var oldUser = GetByUsername(username);
            if (username != newUser.Credentials.Username
                || newUser.Credentials.Password != oldUser.Credentials.Password)
            {
                throw new InvalidCredentialsException();
            }

            oldUser.Profile = newUser.Profile;
            _context.Users.Update(oldUser);
            _context.SaveChanges();

            return oldUser;
        }

        public void FollowUser(CredentialsDto follower, string followee)
        {
            var followingUser = GetByUsername(follower.Username);
            var followeeUser = GetByUsername(followee);
            var existingRelationship = _context.Following
                .SingleOrDefault( f => f.FollowerId == followingUser.Id && f.FolloweeId == followeeUser.Id);
            if (existingRelationship != null)
            {
                throw new InvalidCredentialsException();
            }
            var relationship = new Following { FolloweeId = followeeUser.Id, FollowerId = followingUser.Id };
            _context.Following.Add(relationship);
            _context.SaveChanges();
        }

        public void Unfollow(CredentialsDto follower, string followee)
        {
            var followingUser = GetByUsername(follower.Username);
            var followeeUser = GetByUsername(followee);
            var existingRelationship = _context.Following 
                .SingleOrDefault( r => r.FollowerId == followingUser.Id && r.FolloweeId == followeeUser.Id);
            if (existingRelationship == null)
            {
                throw new InvalidCredentialsException();
            }
            _context.Following.Remove(existingRelationship);
            _context.SaveChanges();
        }

        public IEnumerable<User> Following(string follower)
        {
            var followingUser = GetByUsername(follower);
            var userIds = _context.Following
                .Where( r => r.FollowerId == followingUser.Id).Select( i => i.FolloweeId ).ToList();
            return _context.Users.Where( u => userIds.Contains(u.Id)).ToList();
        }

        public IEnumerable<User> GetFollowers(string follower)
        {
            var followedUser = GetByUsername(follower);
            var userIds = _context.Following
                .Where( r => r.FolloweeId == followedUser.Id).Select( i => i.FollowerId ).ToList();
            return _context.Users.Where( u => userIds.Contains(u.Id)).ToList();
        }



        public User DeleteUser(string username, CredentialsDto credentials)
        {
            // Get user if username matches and user is not deleted
            var user = _context.Users
                .SingleOrDefault(u => u.Credentials.Username == username
                                      && !u.Deleted);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.Credentials.Username != credentials.Username
                || user.Credentials.Password != credentials.Password)
            {
                throw new InvalidCredentialsException();
            }

            user.Deleted = true;
            _context.SaveChanges();
            return user;
        }

        public bool UsernameExists (string username)
        {
            return GetByUsernameOrDefault(username) == null 
                ? false
                : true;
        }

        public bool UsernameAvailable (string username)
        {
            return !UsernameExists(username);
        }

        public void ValidateUser (CredentialsDto credentials)
        {
            var user = GetByUsername(credentials.Username);
            if (user.Credentials.Password != credentials.Password)
            {
                throw new InvalidCredentialsException();
            }
        }

  }
}