using System.Net;
using System.Reflection;
using Domain.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api.Extensions;

public static class FunctionContextExtensions
{
    public static HttpRequestData? GetHttpRequestData(this FunctionContext context)
    {
        var functionBindingsFeature = context
            .Features
            .SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature")
            .Value;
        var type = functionBindingsFeature.GetType();
        var inputData = type
            .GetProperties()
            .Single(p => p.Name == "InputData")
            .GetValue(functionBindingsFeature) as IReadOnlyDictionary<string, object>;
        return inputData?
            .Values
            .SingleOrDefault(o => o is HttpRequestData) as HttpRequestData;
    }

    public static async Task<HttpResponseData> CreateExceptionResponseAsync(this HttpRequestData request, Result result)
    {
        var type = result.Errors!.First().ErrorType;
        var responseMessage = new ProblemDetails
        {
            Type = GetType(type),
            Title = GetTitle(type),
            Status = GetStatusCodeInt(type),
            Extensions = new Dictionary<string, object?>
            {
                { "errors", new[] { result.Errors } }
            }
        };
        var response = request.CreateResponse();
        var code = GetStatusCode(type);
        await response.WriteAsJsonAsync(responseMessage, code);
        return response;
    }

    private static int GetStatusCodeInt(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => (int)HttpStatusCode.BadRequest,
            ErrorType.NotFound => (int)HttpStatusCode.NotFound,
            ErrorType.Conflict => (int)HttpStatusCode.Conflict,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }

    private static HttpStatusCode GetStatusCode(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => HttpStatusCode.BadRequest,
            ErrorType.NotFound => HttpStatusCode.NotFound,
            ErrorType.Conflict => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };
    }

    private static string GetTitle(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            _ => "Server Failure"
        };
    }

    private static string GetType(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };
    }

    public static void InvokeResult(this FunctionContext context, HttpResponseData response)
    {
        var functionBindingsFeature = context
            .Features
            .SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature")
            .Value;
        var type = functionBindingsFeature.GetType();
        var result = type
            .GetProperties()
            .Single(p => p.Name == "InvocationResult");
        result.SetValue(functionBindingsFeature, response);
    }

    public static MethodInfo? GetTargetFunctionMethod(this FunctionContext context)
    {
        var entryPoint = context.FunctionDefinition.EntryPoint;
        var assembly = Assembly.LoadFrom(context.FunctionDefinition.PathToAssembly);
        var firstName = entryPoint[..entryPoint.LastIndexOf('.')];
        var type = assembly.GetType(firstName);
        var secondName = entryPoint[(entryPoint.LastIndexOf('.') + 1)..];
        return type?.GetMethod(secondName) ?? null;
    }
}