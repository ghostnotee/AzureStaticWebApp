using Contracts.Auth;
using Domain.Users;
using Mapster;

namespace Application.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(User User, string Token), AuthenticationResult>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}