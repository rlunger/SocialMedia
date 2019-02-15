using System;
namespace DotnetAssessmentSocialMedia.Dtos
{
    public class TweetResponseDto
    {
        public int Author { get; set;}
        public DateTime Posted { get; set; }
        public string Content { get; set; }
        public int InReplyOf { get; set; }
        public int RepostOf { get; set; }
        
    }
}