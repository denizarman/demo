using AutoMapper;
using Demo.Api.Attributes;
using Demo.Api.Dtos;
using Demo.Core.Models;
using Demo.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequestDto request)
        {
            var response = _userService.Authenticate(_mapper.Map<LoginRequestModel>(request)).Result;
            return Ok(_mapper.Map<AuthenticationResponseDto>(response));
        }

        [HttpGet("amiadmin")]
        [Authorize("admin")]
        public IActionResult AmIAdmin()
        {
            return Ok();
        }
    }
}
