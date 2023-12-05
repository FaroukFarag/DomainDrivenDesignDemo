using Application.Services.Authentication;
using Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public IActionResult Resister(RegisterRequest registerRequest)
        {
            var authenticationResult = _authenticationService.Register(
                registerRequest.FirstName,
                registerRequest.LastName,
                registerRequest.Email,
                registerRequest.Password);

            var response = new AuthenticationResponse(
                authenticationResult.User.Id,
                authenticationResult.User.FirstName,
                authenticationResult.User.LastName,
                authenticationResult.User.Email,
                authenticationResult.Token);

            return Ok(response);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var authenticationResult = _authenticationService.Login(
                loginRequest.Email,
                loginRequest.Password);

            var response = new AuthenticationResponse(
                authenticationResult.User.Id,
                authenticationResult.User.FirstName,
                authenticationResult.User.LastName,
                authenticationResult.User.Email,
                authenticationResult.Token);

            return Ok(response);
        }
    }
}
