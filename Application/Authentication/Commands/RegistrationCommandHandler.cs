using Application.Common.Interfaces.Authentication;
using Contracts.Auth;
using Domain.Shared;
using Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands;

public record RegistrationCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Result<AuthenticationResult>>;

public class RegistrationCommandHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
    : IRequestHandler<RegistrationCommand, Result<AuthenticationResult>>
{
    public async Task<Result<AuthenticationResult>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User(request.FirstName, null, request.LastName, request.Email);
        var result = await userManager.CreateAsync(newUser, request.Password);
        return !result.Succeeded
            ? Result.Failure<AuthenticationResult>(result.Errors.Select(x => new Error(x.Code, x.Description, ErrorType.Validation)))
            : mapper.Map<AuthenticationResult>((newUser, jwtTokenGenerator.GenerateToken(newUser)));
    }
}