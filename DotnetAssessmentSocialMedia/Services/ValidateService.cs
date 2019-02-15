using Microsoft.Extensions.Logging;

namespace DotnetAssessmentSocialMedia.Services
{
    public class ValidateService : IValidateService
    {

        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IHashtagService _hashtagService;

        public ValidateService(IUserService userService, IHashtagService hashtagService, ILogger<ValidateService> logger)
        {
            _userService = userService;
            _hashtagService = hashtagService;
            _logger = logger;
        }

        public bool UsernameAvailable(string username)
        {
            return _userService.UsernameAvailable(username);
        }

        public bool UsernameExists(string username)
        {
            return _userService.UsernameExists(username);
        }

        public bool HashtagExists(string label)
        {
            return _hashtagService.GetHashtagByLabel(label) != null;
        }
    }
}
