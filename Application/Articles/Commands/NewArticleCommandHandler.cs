// ReSharper disable SuggestBaseTypeForParameterInConstructor

using Application.Common.Interfaces.Persistence;
using Contracts.Articles;
using Contracts.Auth;
using Domain.Articles;
using Domain.Articles.Entities;
using Domain.Shared;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Articles.Commands;

public record NewArticleCommand(Guid CategoryId, string Title, string ContentSummary, string ContentMain)
    : IRequest<Result<ArticleDto>>;

public class NewArticleCommandHandler(
    UserInfo userInfo,
    ISqlRepository<Article> articles,
    ISqlRepository<ArticleCategory> articleCategories,
    IMapper mapper)
    : IRequestHandler<NewArticleCommand, Result<ArticleDto>>
{
    public Task<Result<ArticleDto>> Handle(NewArticleCommand request, CancellationToken cancellationToken)
    {
        var categoryName = articleCategories.AsNoTracking().FirstAsync(x => x.Id == request.CategoryId, cancellationToken).Result.Name;
        var newArticle = new Article(userInfo.Id, request.CategoryId, request.Title, request.ContentSummary, request.ContentMain);
        articles.Add(newArticle);
        return Task.FromResult(Result.Success(mapper.Map<ArticleDto>((newArticle, userInfo.Name, categoryName))));
    }
}

public class NewArticleCommandValidator : AbstractValidator<NewArticleCommand>
{
    public NewArticleCommandValidator()
    {
        RuleFor(x => x.CategoryId).NotNull().NotEmpty();
        RuleFor(x => x.Title).NotNull().NotEmpty();
        RuleFor(x => x.ContentSummary).NotNull().NotEmpty();
        RuleFor(x => x.ContentMain).NotNull().NotEmpty();
    }
}