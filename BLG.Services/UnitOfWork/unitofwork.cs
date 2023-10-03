using BLG.Domin.CategoryBlogAgg;
using BLG.Domin.PostBlogAgg;
using BLG.Domin.uploadImage;
using BLG.Infrastructure.Context;
using BLG.Services.GenericReposetory;

namespace BLG.Services.UnitOfWork;

public class unitofwork:Iunitofwork,IDisposable
{
    private readonly ApplicationContext _context;
    private genericrepository<Postblog> _postblog;
    private genericrepository<category> _category;
    private genericrepository<uploadimg> _upldadimg;

    public unitofwork(ApplicationContext context)
    {
        _context = context;
    }

    public genericrepository<Postblog> postbloguw
    {
        get
        {
            if (_postblog==null)
            {
                _postblog = new genericrepository<Postblog>(_context);
            }

            return _postblog;
        }
    }

    public genericrepository<category> categoryuw
    {
        get
        {
            if (_category==null)
            {
                _category = new genericrepository<category>(_context);
            }

            return _category;
        }
    }

    public genericrepository<uploadimg> uploadimguw
    {
        get
        {
            if (_upldadimg==null)
            {
                _upldadimg = new genericrepository<uploadimg>(_context);
            }

            return _upldadimg;
        }
    }
     
    public void Dispose()
    {
       _context.Dispose();
    }

    public void save()
    {
        _context.SaveChanges();
    }

  public  void saveAsync()
    {
        _context.SaveChangesAsync();
    }
}