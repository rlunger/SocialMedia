using System.Collections.Generic;
using DotnetAssessmentSocialMedia.Data.Entities;
using DotnetAssessmentSocialMedia.Dtos;

namespace DotnetAssessmentSocialMedia.Services
{
    public interface IValidateService
    {
        bool UsernameAvailable (string username);
        bool UsernameExists (string username);
        
        bool HashtagExists (string label);
    }
}