using Api.Extensions;
using Domain.Shared;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace Api.Middlewares;

public class ExceptionMiddleware(/*ILogger<ExceptionMiddleware> logger*/) : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            var requestData = context.GetHttpRequestData();
            if (requestData is null) return;
            var response = await requestData.CreateExceptionResponseAsync(Result.Failure<Exception>(
                ex.Errors.Select(x => Error.Validation(x.ErrorCode, x.ErrorMessage))));
            context.InvokeResult(response);
        }
        catch (Exception ex)
        {
            //logger.LogError($"Exception occurred in {context.FunctionDefinition.Name}: {ex.Message}");
            await CreateExceptionResponse(context, ex.Message, ex.InnerException?.Message);
        }
    }

    private static async Task CreateExceptionResponse(FunctionContext context, string? exceptionMessage, string? innerExceptionMessage = null)
    {
        var requestData = context.GetHttpRequestData();
        if (requestData is null) return;
        var response = await requestData.CreateExceptionResponseAsync(Result.Failure<Exception>(new List<Error>
            {
                Error.Failure("Server Error", innerExceptionMessage ?? exceptionMessage)
            }
        ));
        context.InvokeResult(response);
    }
}