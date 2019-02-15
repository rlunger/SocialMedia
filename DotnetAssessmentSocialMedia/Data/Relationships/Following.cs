using DotnetAssessmentSocialMedia.Data.Entities;
namespace DotnetAssessmentSocialMedia.Data.Relationships
{
    public class Following
    {
        public int FollowingId { get; set; }
        public int FolloweeId { get; set; }
        // public User Followee { get; set; }
        public int FollowerId { get; set; }
        // public User Follower { get; set; }
    }
}