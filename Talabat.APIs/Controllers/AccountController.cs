using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Dtos.Auth;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly IUserService _user;

        public AccountController(IUserService user)
        {
            _user = user;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userDto = await _user.LoginAsync(loginDto);
            if (userDto is null)
            {
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));
            }
            return Ok(userDto);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userDto = await _user.RegisterAsync(registerDto);
            if (userDto is null) 
            { 
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));
            }
            else
            {
                return Ok(userDto);
            }
        }
    }
}
