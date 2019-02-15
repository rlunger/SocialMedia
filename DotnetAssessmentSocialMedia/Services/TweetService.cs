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
using DotnetAssessmentSocialMedia;

namespace DotnetAssessmentSocialMedia.Services
{
    public class TweetService : ITweetService
    {
        private readonly SocialMediaContext _context;
        private readonly IHashtagService _hashtagService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        private void _processContent (Tweet tweet)
        {
          foreach (var hashtag in Util.Parse.GetHashtags(tweet.Content))
          {
            _hashtagService.UpdateHashtag(hashtag);
          }

          foreach (var mentionedUser in Util.Parse.GetMentions(tweet.Content))
          {
            var user = _userService.GetByUsernameOrDefault(mentionedUser);
            if (user != null)
            {
              var mention = new Mentions {
                MentionedUser = user.Id,
                MentioningTweet = tweet.Id
              };

              _context.Mentions.Add(mention);
          
            }
          }
        }


        public TweetService(SocialMediaContext context, IHashtagService hashtagService, IUserService userService, ILogger<UserService> logger)
        {
            _context = context;
            _hashtagService = hashtagService;
            _userService = userService;
            _logger = logger;
        }

        public IEnumerable<Tweet> GetAllTweets()
        {
          return _context.Tweets.Where( t => t.Deleted == false ).ToList();
        }

        public Tweet CreateTweet(User author, string content)
        {
          var tweet = new Tweet {
            Author = author.Id,
            Posted = DateTime.Now,
            Content = content,
          };

          _context.Tweets.Add(tweet);
          _context.SaveChanges();
          _processContent(tweet);
          return tweet;
        }

        public Tweet CreateRepost(User reposter, int originalId)
        {
          var tweet = new Tweet {
            Author = reposter.Id,
            Posted = DateTime.Now,
            RepostOf = originalId,
          };

          _context.Tweets.Add(tweet);
          _context.SaveChanges();
          return tweet;
        }

        public IEnumerable<Tweet> GetTweetsByUser (User user)
        {
          return _context.Tweets.Where( t => t.Author == user.Id ).ToList();
        }

        public IEnumerable<Tweet> GetTweetsByHashtag (string label)
        {
          return _context.Tweets.Where( t => t.Content.Contains("#" + label) ).ToList();
        }

  }
}