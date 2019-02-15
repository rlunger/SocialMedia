using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAssessmentSocialMedia.Data.Entities
{
    public class Tweet
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int Author { get; set;}
        public DateTime Posted { get; set; }
        public string Content { get; set; }
        [ForeignKey("Tweet")]
        public int InReplyOf { get; set; }
        [ForeignKey("Tweet")]
        public int RepostOf { get; set; }

        public bool Deleted { get; set; } = false;
    }
}