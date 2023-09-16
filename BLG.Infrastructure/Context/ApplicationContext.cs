using BLG.Domin.CategoryBlogAgg;
using BLG.Domin.PostBlogAgg;
using BLG.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BLG.Infrastructure.Context;

public class ApplicationContext:DbContext
{
    public DbSet<Postblog> postblog { get; set; }
    public DbSet<category> category { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new Blogpostmapping());
        modelBuilder.ApplyConfiguration(new categorymapping());
    }
}