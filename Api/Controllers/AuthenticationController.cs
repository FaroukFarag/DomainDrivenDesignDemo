using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Application.Common.Errors;
using Contracts.Authentication;
using Domain.Common.Errors;
using ErrorOr;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public AuthenticationController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var command = _mapper.Map<RegisterCommand>(registerRequest);

            ErrorOr<AuthenticationResult> registerResult = await _sender.Send(command);

            return registerResult.Match(
                registerResult => Ok(_mapper.Map<AuthenticationResponse>(registerResult)),
                errors => Problem(errors));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var query = _mapper.Map<LoginQuery>(loginRequest);
            var authenticationResult = await _sender.Send(query);

            if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authenticationResult.FirstError.Description);

            return authenticationResult.Match(
                authenticationResult => Ok(_mapper.Map<AuthenticationResponse>(authenticationResult)),
                errors => Problem(errors));
        }
    }
}
