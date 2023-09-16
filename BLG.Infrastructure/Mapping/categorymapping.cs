using BLG.Domin.CategoryBlogAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BLG.Infrastructure.Mapping;

public class categorymapping:IEntityTypeConfiguration<category>
{
    public void Configure(EntityTypeBuilder<category> builder)
    {
        builder.HasKey(x => x.id);
        builder.Property(x => x.name).IsRequired();
        builder.Property(x => x.urlhadle).IsRequired();
    }
}