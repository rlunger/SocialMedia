using DotnetAssessmentSocialMedia.Data.Entities;
using DotnetAssessmentSocialMedia.Data.Relationships;
using Microsoft.EntityFrameworkCore;

namespace DotnetAssessmentSocialMedia.Data
{
    public class SocialMediaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Following> Following { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Mentions> Mentions { get; set; }
        
        public SocialMediaContext(DbContextOptions<SocialMediaContext> options)
            : base(options) 
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credentials>()
                .HasAlternateKey(c => c.Username);
            
            // modelBuilder.Entity<Following>()
            //     .HasKey( t => new { t.FolloweeId, t.FollowerId });
        }
    }
}