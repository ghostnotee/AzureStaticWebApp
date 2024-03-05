using Application.Common.Interfaces.Authentication;
using Contracts.Auth;
using Domain.Shared;
using Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Queries;

public record LoginQuery(string Email, string Password) : IRequest<Result<AuthenticationResult>>;

public class LoginQueryHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
    : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    public async Task<Result<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            return Result.Failure<AuthenticationResult>(new List<Error> { UserErrors.Failure });
        var token = jwtTokenGenerator.GenerateToken(user);
        return mapper.Map<AuthenticationResult>((user, token));
    }
}