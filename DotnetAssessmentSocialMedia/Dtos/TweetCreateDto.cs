using System.ComponentModel.DataAnnotations;

namespace DotnetAssessmentSocialMedia.Dtos
{
    public class TweetCreateDto
    {

        [Required]
        public string Content { get; set; }
        
        [Required]
        public CredentialsDto Credentials { get; set; }

    }
}