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
    [Route("api/tags")]
    [ApiController]
    public class HashtagController : ControllerBase
    {
        private readonly IHashtagService _hashtagService;

        private readonly ITweetService _tweetService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;
        
        public HashtagController(IHashtagService hashtagService, ITweetService tweetService, IMapper mapper, ILogger<HashtagController> logger)
        {
            _hashtagService = hashtagService;
            _tweetService = tweetService;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<Hashtag> GetAllHashtags ()
        {
            return _hashtagService.GetAllHashtags();
        }

        // public Hashtag GetHashtag (string label)
        // {
        //     return _hashtagService.GetHashtag(label);
        // }

        [HttpGet("{label}")]
        public IEnumerable<TweetCreateDto> GetTweetByHashtag (string label)
        {
            return _mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetCreateDto>>(_tweetService.GetTweetsByHashtag(label)).ToList();
        }

    }
}