using System.Net;
using Api.Extensions;
using Application.Authentication.Commands;
using Application.Authentication.Queries;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api.EndPoints;

public class AuthenticationFunctions(ISender sender)
{
    [Function("Register")]
    public async Task<HttpResponseData> Register([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth/register")] HttpRequestData req)
    {
        var command = await req.GetValueFromBody<RegistrationCommand>();
        var result = await sender.Send(command);
        return await req.ToHttpResponseData(result, HttpStatusCode.Created);
    }

    [Function("Login")]
    public async Task<HttpResponseData> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth/login")] HttpRequestData req)
    {
        var command = await req.GetValueFromBody<LoginQuery>();
        var result = await sender.Send(command);
        return await req.ToHttpResponseData(result);
    }
}