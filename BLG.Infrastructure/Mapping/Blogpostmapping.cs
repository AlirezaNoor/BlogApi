using BLG.Domin.PostBlogAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BLG.Infrastructure.Mapping;

public class Blogpostmapping:IEntityTypeConfiguration<Postblog>
{
    public void Configure(EntityTypeBuilder<Postblog> builder)
    {
        builder.HasKey(x => x.id);
        builder.Property(x => x.cotent).IsRequired();
        builder.Property(x => x.date).IsRequired();
        builder.Property(x => x.img).IsRequired();
        builder.Property(x => x.shorttitle).IsRequired();
        builder.Property(x => x.isvisible).IsRequired();
        builder.Property(x => x.title).IsRequired();
        builder.Property(x => x.urlhandler).IsRequired();
        builder.Property(x => x.Author).IsRequired();
        builder.HasMany(x => x.Categories).WithMany(x => x.posts);
    }
}