using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Api.Attributes;
using Api.Extensions;
using Contracts.Auth;
using Domain.Shared;
using Infrastructure.Settings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.IdentityModel.Tokens;

namespace Api.Middlewares;

public class AuthMiddleware(AppSettings appSettings) : IFunctionsWorkerMiddleware
{
    private TokenValidationParameters _validationParameters = null!;

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var (isAuthRequired, role) = CheckAuthorizeRequirement(context);
        if (!isAuthRequired)
        {
            await next(context);
            return;
        }

        var headerData = context.BindingContext.BindingData["headers"] as string;
        var headers = JsonSerializer.Deserialize<Dictionary<string, string>>(headerData!);
        
        // TODO : Bearer token validation condition 
        
        if (!headers!.TryGetValue("Authorization", out var authorization))
        {
            await CreateExceptionResponse(context, HttpStatusCode.BadRequest, "Token must be provided");
            return;
        }

        var bearerHeader = AuthenticationHeaderValue.Parse(authorization);
        var (isAuthenticated, httpStatusCode) = await Authenticate(bearerHeader, role);
        if (!isAuthenticated)
        {
            await CreateExceptionResponse(context, httpStatusCode);
            return;
        }

        SetUserInfo(context, bearerHeader);
        await next(context);
    }

    private (bool, string) CheckAuthorizeRequirement(FunctionContext context)
    {
        var targetFunctionMethod = context.GetTargetFunctionMethod();
        if (targetFunctionMethod == null) return (false, "");
        return Attribute.GetCustomAttribute(targetFunctionMethod, typeof(AuthorizeAttribute)) is not AuthorizeAttribute attribute
            ? (false, "")
            : (true, attribute.Role);
    }

    private static async Task CreateExceptionResponse(FunctionContext context, HttpStatusCode statusCode, string? errorMessage = null)
    {
        var request = context.GetHttpRequestData();
        if (request is null) return;
        var response = await request.CreateExceptionResponseAsync(Result.Failure(new[] { Error.Validation("Error.Request", errorMessage) }));
        context.InvokeResult(response);
    }

    private async Task<(bool, HttpStatusCode)> Authenticate(AuthenticationHeaderValue bearerHeader, string role)
    {
        var (token, principal) = await Validate(bearerHeader.Parameter, role);
        var userInRole = role == "All" || principal.FindAll(c => c.Type == ClaimTypes.Role).Select(c => c.Value).Any(x => x == role);
        var isAuthenticated = principal.Identity!.IsAuthenticated;
        if (!isAuthenticated) return (false, HttpStatusCode.Unauthorized);
        return !userInRole ? (false, HttpStatusCode.Forbidden) : (true, HttpStatusCode.OK);
    }

    private async Task<(JwtSecurityToken, ClaimsPrincipal)> Validate(string? token, string role)
    {
        await ConfigureValidation();
        var tokenHandler = new JwtSecurityTokenHandler();
        var result = tokenHandler.ValidateToken(token, _validationParameters, out var jwt);
        return (jwt as JwtSecurityToken, result)!;
    }

    private Task ConfigureValidation()
    {
        _validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = appSettings.Issuer,
            ValidAudience = appSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret!))
        };
        return Task.CompletedTask;
    }

    private void SetUserInfo(FunctionContext context, AuthenticationHeaderValue token)
    {
        var userInfo = context.InstanceServices.GetService(typeof(UserInfo)) as UserInfo;
        var handler = new JwtSecurityTokenHandler();
        var currentUser = handler.ReadJwtToken(token.Parameter);
        var userId = Guid.Parse(currentUser.Claims.First(x => x.Type.Equals(JwtRegisteredClaimNames.Sub)).Value);
        var name = currentUser.Claims.First(x => x.Type.Equals(ClaimTypes.Name)).Value;
        userInfo!.SetUserInformation(userId, name, token.Parameter!);
    }
}