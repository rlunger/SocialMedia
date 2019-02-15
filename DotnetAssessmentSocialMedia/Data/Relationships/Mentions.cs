using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAssessmentSocialMedia.Data.Relationships
{
    public class Mentions
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int MentionedUser { get; set; }

        [ForeignKey("Tweet")]
        public int MentioningTweet { get; set; }
    }
}