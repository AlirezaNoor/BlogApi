using BLG.Domin.PostBlogAgg;
using BLG.Infrastructure.Context;
using BLG.Infrastructure.customRepository;
using Microsoft.EntityFrameworkCore;

namespace BLG.Services.Customrepository;

public class PostblogReposetory:IPostblogReposetory
{
    public readonly ApplicationContext _context;

    public PostblogReposetory(ApplicationContext context)
    {
        _context = context;
    }

    public virtual void updatepostblog( Postblog pb)
    {
       var beforupdate= _context.postblog.Include(x => x.Categories).FirstOrDefault(x => x.id == pb.id);
        _context.Entry(beforupdate).CurrentValues.SetValues(pb);
        beforupdate.Categories = pb.Categories;
        _context.SaveChanges();
    }
    
}