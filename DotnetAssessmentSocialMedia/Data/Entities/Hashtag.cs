using System;
namespace DotnetAssessmentSocialMedia.Data.Entities
{
    public class Hashtag
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public DateTime FirstUsed { get; set; }
        public DateTime LastUsed { get; set; }
    }
}