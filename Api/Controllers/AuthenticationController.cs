using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Application.Common.Errors;
using Contracts.Authentication;
using Domain.Common.Errors;
using ErrorOr;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var query = new LoginQuery(loginRequest.Email, loginRequest.Password);
            var authenticationResult = await _sender.Send(query);

            if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authenticationResult.FirstError.Description);

            return authenticationResult.Match(
                authenticationResult => Ok(MapAuthenticationResult(authenticationResult)),
                errors => Problem(errors));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var command = new RegisterCommand(
                registerRequest.FirstName,
                registerRequest.LastName,
                registerRequest.Email,
                registerRequest.Password);

            ErrorOr<AuthenticationResult> registerResult = await _sender.Send(command);

            return registerResult.Match(
                registerResult => Ok(MapAuthenticationResult(registerResult)),
                errors => Problem(errors));
        }

        private static AuthenticationResponse MapAuthenticationResult(AuthenticationResult authenticationResult)
        {
            return new AuthenticationResponse(
                            authenticationResult.User.Id,
                            authenticationResult.User.FirstName,
                            authenticationResult.User.LastName,
                            authenticationResult.User.Email,
                            authenticationResult.Token);
        }
    }
}
