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
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ITweetService _tweetService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;
        
        public UserController(IUserService userService, ITweetService tweetService, IMapper mapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _tweetService = tweetService;
            _mapper = mapper;
            _logger = logger;
        }
        
        // GET api/users
        [HttpGet]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<UserResponseDto>> Get()
        {
            var result = _userService.GetAll();
            var users = result.ToList();

            var mappedUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserResponseDto>>(users);
            return mappedUsers.ToList();
        }

        // GET api/users/@{username}
        [HttpGet("@{username}")]
        [ProducesResponseType(404)]
        public ActionResult<UserResponseDto> Get(string username)
        {
            var user = _userService.GetByUsername(username);
            return _mapper.Map<User, UserResponseDto>(user);
        }

        // POST api/users
        [HttpPost]
        [ProducesResponseType(400)]
        public ActionResult<UserResponseDto> Post([FromBody] CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return _mapper.Map<UserResponseDto>(_userService.CreateUser(user));
        }

        [HttpPatch("@{username}")]
        public UserResponseDto PatchUser(string username, [FromBody] CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return _mapper.Map<UserResponseDto>(_userService.UpdateUser(username, user));
        }

        [HttpPost("@{followee}/follow")]
        public void FollowUser([FromBody] CredentialsDto follower, string followee)
        {
            _userService.FollowUser(follower, followee);
        }

        [HttpGet("@{username}/following")]
        public IEnumerable<UserResponseDto> Following(string username)
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserResponseDto>>(_userService.Following(username)).ToList();
        }

        [HttpGet("@{username}/followers")]
        public IEnumerable<UserResponseDto> GetFollowers(string username)
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserResponseDto>>(_userService.GetFollowers(username)).ToList();
        }

        [HttpPost("@{username}/unfollow")]
        public void Unfollow([FromBody] CredentialsDto follower, string followee)
        {
            _userService.Unfollow(follower, followee);
        }

        // DELETE api/users/@{username}
        [HttpDelete("@{username}")]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public UserResponseDto Delete(string username, [FromBody] CredentialsDto credentialsDto)
        {
            var credentials = _mapper.Map<CredentialsDto>(credentialsDto);
            return _mapper.Map<UserResponseDto>(_userService.DeleteUser(username, credentials));
        }

        [HttpGet("@{username}/tweets")]
        public IEnumerable<Tweet> GetTweetsByUser(string username)
        {
            var user = _userService.GetByUsername(username);
            return _tweetService.GetTweetsByUser(user);
        }
    }
}