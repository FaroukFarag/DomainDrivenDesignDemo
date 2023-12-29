using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Contracts.Authentication;
using Mapster;

namespace Api.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();
            
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(des => des, src => src.User);
        }
    }
}
