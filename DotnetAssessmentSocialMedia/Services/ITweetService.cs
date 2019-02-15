using System.Collections.Generic;
using DotnetAssessmentSocialMedia.Data.Entities;
using DotnetAssessmentSocialMedia.Dtos;

namespace DotnetAssessmentSocialMedia.Services
{
    public interface ITweetService
    {
        IEnumerable<Tweet> GetAllTweets ();

        Tweet CreateTweet (User author, string content);

        Tweet CreateRepost (User reposter, int originalId);

        IEnumerable<Tweet> GetTweetsByUser (User user);

        IEnumerable<Tweet> GetTweetsByHashtag (string label);
    }
}