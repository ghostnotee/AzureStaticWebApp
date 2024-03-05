using Domain.Articles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ArticleConfigurations : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.ContentSummary).HasMaxLength(250);
        builder.Property(x => x.CreatedDate).ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.CreatorId);
    }
}