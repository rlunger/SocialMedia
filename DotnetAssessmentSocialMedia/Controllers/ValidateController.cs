using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DotnetAssessmentSocialMedia.Data.Entities;
using DotnetAssessmentSocialMedia.Dtos;
using DotnetAssessmentSocialMedia.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotnetAssessmentSocialMedia.Controllers
{
    [Route("api/validate")]
    [ApiController]
    public class ValidateController : ControllerBase
    {
        private readonly IValidateService _validateService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;
        
        public ValidateController(IValidateService validateService, IMapper mapper, ILogger<ValidateController> logger)
        {
            _validateService = validateService;
            _mapper = mapper;
            _logger = logger;
        }
        
        // GET api/users
        [HttpGet("username/exists/@{username}")]
        public bool ValidateUsernameExists (string username)
        {
            return _validateService.UsernameExists(username);
        }

        [HttpGet("username/available/@{username}")]
        public bool ValidateUsernameAvailable (string username)
        {
            return _validateService.UsernameAvailable(username);
        }

        [HttpGet("tag/exists/{label}")]
        public bool ValidateTagExists (string label)
        {
            return _validateService.HashtagExists(label);
        }


    }
}