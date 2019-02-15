using System.Collections.Generic;
using DotnetAssessmentSocialMedia.Data.Entities;
using DotnetAssessmentSocialMedia.Dtos;

namespace DotnetAssessmentSocialMedia.Services
{
    public interface IUserService
    {
        User GetByUsername(string username);
        User GetByUsernameOrDefault(string username);
        IEnumerable<User> GetAll();
        User CreateUser(User user);
        User DeleteUser(string username, CredentialsDto credentials);

        User UpdateUser(string username, User user);

        void FollowUser(CredentialsDto follower, string followee);

        void Unfollow(CredentialsDto follower, string followee);

        IEnumerable<User> Following(string username);

        IEnumerable<User> GetFollowers(string username);

        bool UsernameExists (string username);
        bool UsernameAvailable (string username);

        void ValidateUser (CredentialsDto credentials);
    }
}