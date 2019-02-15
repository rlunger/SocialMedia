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
    public class HashtagService : IHashtagService
    {
        private readonly SocialMediaContext _context;

        private readonly ILogger _logger;

        public HashtagService(SocialMediaContext context, ILogger<HashtagService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Hashtag> GetAllHashtags()
        {
          return _context.Hashtags.ToList();
        }

        public Hashtag GetHashtagByLabel(string label)
        {
          return _context.Hashtags.SingleOrDefault( tag => tag.Label == label);
        }

        public void UpdateHashtag(string label)
        {
          var hashtag = GetHashtagByLabel(label);
          if (hashtag == null)
          {
              hashtag = new Hashtag { 
                  Label = label,
                  FirstUsed = DateTime.Now,
                  LastUsed = DateTime.Now
              };

              _context.Hashtags.Add(hashtag);
              _context.SaveChanges();
          }
          else {
            hashtag.LastUsed = DateTime.Now;
            _context.Hashtags.Update(hashtag);
            _context.SaveChanges();
          }
        }
  }
}