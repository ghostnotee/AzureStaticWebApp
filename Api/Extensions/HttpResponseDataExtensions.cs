using System.Net;
using Domain.Shared;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api.Extensions;

public static class HttpResponseDataExtensions
{
    public static async Task<HttpResponseData> ToHttpResponseData<TValue>(this HttpRequestData request, Result<TValue> result, HttpStatusCode? code = null)
    {
        if (!result.IsSuccess)
        {
            var responseFail = await request.CreateExceptionResponseAsync(result);
            return responseFail;
        }
        var response = request.CreateResponse();
        code ??= HttpStatusCode.OK;
        await response.WriteAsJsonAsync(result.Value, code.Value);
        return response;
    }
}