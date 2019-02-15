using System;
using System.Collections.Generic;
using DotnetAssessmentSocialMedia.Data.Relationships;

namespace DotnetAssessmentSocialMedia.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        public Profile Profile { get; set; }
        
        public Credentials Credentials { get; set; }
        
        public DateTime Joined { get; set; }

        public Boolean Deleted { get; set; }

        // public ICollection<Following> Following { get; } = new List<Following>();
        //public ICollection<Following> Followers { get; } = new List<Following>();
    }
}