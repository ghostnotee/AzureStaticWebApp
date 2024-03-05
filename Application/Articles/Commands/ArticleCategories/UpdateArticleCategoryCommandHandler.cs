using Application.Common.Interfaces.Persistence;
using Domain.Articles.Entities;
using Domain.Shared;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Articles.Commands.ArticleCategories;

public record UpdateArticleCategoryCommand(Guid? Id, Guid? ParentId, string Name) : IRequest<Result<Unit>>;

public class UpdateArticleCategoryCommandHandler(ISqlRepository<ArticleCategory> articleCategories)
    : IRequestHandler<UpdateArticleCategoryCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await articleCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (articleCategory is null) return Result.Failure<Unit>(new List<Error> { Error.NullValue });
        articleCategory.UpdateCategory(request.ParentId, request.Name);
        return Unit.Value;
    }
}

public class UpdateArticleCategoryCommandValidator : AbstractValidator<NewArticleCategoryCommand>
{
    public UpdateArticleCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}