using Application.Common.Interfaces.Persistence;
using Contracts.Articles;
using Domain.Articles.Entities;
using Domain.Shared;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Articles.Commands.ArticleCategories;

public record NewArticleCategoryCommand(Guid? ParentId, string Name) : IRequest<Result<ArticleCategoryDto>>;

public class NewArticleCategoryCommandHandler(ISqlRepository<ArticleCategory> articleCategories, IMapper mapper)
    : IRequestHandler<NewArticleCategoryCommand, Result<ArticleCategoryDto>>
{
    public async Task<Result<ArticleCategoryDto>> Handle(NewArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await articleCategories.AnyAsync(x => x.Name == request.Name.Trim(), cancellationToken))
            return Result.Failure<ArticleCategoryDto>(new[] { Error.Conflict("Error.Conflict", "Verilen isimle bir kategori zaten mevcut.") });
        var newArticleCategory = new ArticleCategory(request.ParentId, request.Name.Trim());
        articleCategories.Add(newArticleCategory);
        return Result.Success(mapper.Map<ArticleCategoryDto>(newArticleCategory));
    }
}

public class NewArticleCategoryCommandValidator : AbstractValidator<NewArticleCategoryCommand>
{
    public NewArticleCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}