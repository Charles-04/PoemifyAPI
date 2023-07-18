using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poemify.API.Controllers.Shared;
using Poemify.BLL.Interfaces;
using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;

namespace Poemify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("signup",Name ="signup")]
        public async Task<ActionResult<Response<UserRegistrationResponse>>> CreateAccount([FromBody]UserRegistrationRequest userRegistrationRequest)
        {
            var response = await _authService.SignUpAsync(userRegistrationRequest);
            return Ok(response);
        }
        [HttpPost("login",Name ="login")]
        public async Task<ActionResult<Response<LoginResponseDto>>> Login([FromBody]LoginRequestDto loginRequest)
        {
            var response = await _authService.SignIn(loginRequest);
            return Ok(response);
        }

    }
}
