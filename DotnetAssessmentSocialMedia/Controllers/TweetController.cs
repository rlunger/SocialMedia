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
    [Route("api/tweets")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;
        
        public TweetController(ITweetService tweetService, IUserService userService, IMapper mapper, ILogger<TweetController> logger)
        {
            _tweetService = tweetService;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TweetResponseDto> GetAllTweets ()
        {
            return _mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetResponseDto>>(_tweetService.GetAllTweets()).ToList();
        }

        [HttpPost]
        public TweetResponseDto PostTweet([FromBody] TweetCreateDto tweetDto)
        {
            _userService.ValidateUser(tweetDto.Credentials);
            return _mapper.Map<Tweet,TweetResponseDto>(
                _tweetService.CreateTweet(
                    _userService.GetByUsername(tweetDto.Credentials.Username),
                    tweetDto.Content
                ));
        }

        [HttpPost("{id}/repost")]
        public TweetResponseDto RepostTweet([FromBody] CredentialsDto credentialsDto, int id)
        {
            _userService.ValidateUser(credentialsDto);
            return _mapper.Map<Tweet, TweetResponseDto>(
                _tweetService.CreateRepost(
                    _userService.GetByUsername(credentialsDto.Username),
                    id
                )
            );
        }
        
    }
}