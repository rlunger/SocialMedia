using System.Collections.Generic;
using DotnetAssessmentSocialMedia.Data.Entities;
using DotnetAssessmentSocialMedia.Dtos;

namespace DotnetAssessmentSocialMedia.Services
{
    public interface IHashtagService
    {
        IEnumerable<Hashtag> GetAllHashtags ();

        Hashtag GetHashtagByLabel (string label);

        void UpdateHashtag (string label);

        
    }
}