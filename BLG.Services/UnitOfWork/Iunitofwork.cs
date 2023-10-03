using BLG.Domin.CategoryBlogAgg;
using BLG.Domin.PostBlogAgg;
using BLG.Domin.uploadImage;
using BLG.Services.GenericReposetory;

namespace BLG.Services.UnitOfWork;

public interface Iunitofwork
{
    public genericrepository<Postblog> postbloguw { get; }
    public genericrepository<category> categoryuw { get; }
    void save();
    void saveAsync();
    genericrepository<uploadimg> uploadimguw { get; }
}