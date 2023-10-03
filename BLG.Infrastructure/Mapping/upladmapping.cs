using BLG.Domin.uploadImage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BLG.Infrastructure.Mapping;

public class upladmapping:IEntityTypeConfiguration<uploadimg>
{
    public void Configure(EntityTypeBuilder<uploadimg> builder)
    {
        builder.ToTable("uplaodimages");
        builder.HasKey(x => x.id);
    }
}