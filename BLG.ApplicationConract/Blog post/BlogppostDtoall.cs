using BLG.Domin.CategoryBlogAgg;


namespace BLG.ApplicationConract.Blog_post;

public class BlogppostDtoall
{
    public Guid id { get; set; }
    public string title { get; set; }
    public string shorttitle { get; set; }
    public string cotent { get; set; }
    public string img { get; set; }
    public string urlhandler { get; set; }
    public DateTime date { get; set; }
    public string Author { get; set; }
    public bool isvisible { get; set; }
    public ICollection<Domin.CategoryBlogAgg.category> Categories { get; set; }
}