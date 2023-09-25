using BLG.Domin.PostBlogAgg;

namespace BLG.Domin.CategoryBlogAgg;

public class category
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string  urlhadle { get; set; }
    public ICollection<Postblog> posts { get; set; }

}