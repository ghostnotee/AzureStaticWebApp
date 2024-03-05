using System.Net;
using Api.Attributes;
using Api.Extensions;
using Application.Articles.Commands;
using Application.Articles.Commands.ArticleCategories;
using Application.Articles.Queries.ArticleCategories;
using Domain.Shared;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api.EndPoints;

public class ArticleFunctions(ISender sender)
{
    #region Article

    [Authorize]
    [Function("NewArticle")]
    public async Task<HttpResponseData> NewArticle([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "articles")] HttpRequestData req)
    {
        var command = await req.GetValueFromBody<NewArticleCommand>();
        var result = await sender.Send(command);
        return await req.ToHttpResponseData(result, HttpStatusCode.Created);
    }

    [Function("GetArticlesByCategory")]
    public async Task<HttpResponseData> GetArticlesByCategory(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "articles/categories/{id:Guid}")] HttpRequestData req, Guid id)
    {
        return null;

    }

    #endregion

    #region ArticleCategory

    [Authorize]
    [Function("NewArticleCategory")]
    public async Task<HttpResponseData> NewArticleCategory(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "articles/categories")]
        HttpRequestData req)
    {
        var command = await req.GetValueFromBody<NewArticleCategoryCommand>();
        var result = await sender.Send(command);
        return await req.ToHttpResponseData(result, HttpStatusCode.Created);
    }

    [Authorize]
    [Function("UpdateArticleCategory")]
    public async Task<HttpResponseData> UpdateArticleCategory(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "articles/categories/{id:Guid}")]
        HttpRequestData req, Guid id)
    {
        var command = await req.GetValueFromBody<UpdateArticleCategoryCommand>() with { Id = id };
        var result = await sender.Send(command);
        return await req.ToHttpResponseData(result);
    }

    [Authorize]
    [Function("DeleteArticleCategory")]
    public async Task<HttpResponseData> DeleteArticleCategory(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "articles/categories/{id:Guid}")]
        HttpRequestData req, Guid id)
    {
        var result = await sender.Send(new DeleteArticleCategoryCommand(id));
        return await req.ToHttpResponseData<Result>(result);
    }

    [Function("SearchArticleCategory")]
    public async Task<HttpResponseData> SearchArticleCategory(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "articles/categories")]
        HttpRequestData req)
    {
        var name = req.GetValueFromQuery("name");
        var skip = req.GetValueFromQuery("skip");
        byte.TryParse(skip, out var skipByte);
        var take = req.GetValueFromQuery("take");
        byte.TryParse(take, out var takeByte);
        takeByte = (byte)(takeByte > 0 ? takeByte : 10);
        var result = await sender.Send(new SearchArticleCategoryQuery(name, skipByte, takeByte));
        return await req.ToHttpResponseData(result);
    }

    #endregion
}