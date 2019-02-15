using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace DotnetAssessmentSocialMedia.Util
{
    public class Parse
    {
        public static List<string> GetMentions (string content) {
            return Regex.Matches(content, @"@(\w+)").Cast<Match>()
                .Select( m => m.Groups[1].Value)
                .ToList();
        }

        public static List<string> GetHashtags (string content) {
            return Regex.Matches(content, @"#(\w+)")
                .Select( m => m.Groups[1].Value)
                .ToList();
        }
        
    }
}