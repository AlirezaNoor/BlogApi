using BLG.ApplicationConract.Blog_post;
using BLG.Domin.CategoryBlogAgg;
using BLG.Domin.PostBlogAgg;
using BLG.Services.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Blog_WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class PostBlogController : ControllerBase
{
    private readonly Iunitofwork _context;

    public PostBlogController(Iunitofwork context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("postblog")]
    public async Task<IActionResult> createblog(BlogppostDto p)
    {
        if (p == null)
        {
            return BadRequest();
        }

        Postblog post = new()
        {
            cotent = p.cotent,
            date = p.date,
            img = p.img,
            shorttitle = p.shorttitle,
            title = p.title,
            urlhandler = p.urlhandler,
            isvisible = p.isvisible,
            Author = p.Author,
            Categories = new List<category>()
        };
        foreach (var i in p.category)
        {
            var c = _context.categoryuw.Getbyid(i);
            post.Categories.Add(c);
        }
        _context.postbloguw.insert(post);
        _context.save();
        return Ok();
    }

    [HttpGet]
    [Route("postblog")]
    public async ValueTask<IEnumerable<BlogppostDtoall>> getAll()
    {
        var post = _context.postbloguw.get(null,"Categories");
        var lst = new List<BlogppostDtoall>();

        foreach (var p in post)
        {
            lst.Add(new BlogppostDtoall()
            {
                 id = p.id,
                cotent = p.cotent,
                date = p.date,
                img = p.img,
                shorttitle = p.shorttitle,
                title = p.title,
                urlhandler = p.urlhandler,
                isvisible = p.isvisible,
                Author = p.Author,
                Categories = p.Categories.Select(x=>new category
                {
                    id = x.id,
                    name = x.name,
                    urlhadle = x.urlhadle
                    
                }).ToList()
                
            });
        }

        return lst;
    }
    [HttpGet]
    [Route("postblog/{id}")]
    public async ValueTask<BlogppostDtoall> get(Guid id)
    {
        var p=_context.postbloguw.get(x=>x.id==id,"Categories").FirstOrDefault();
        BlogppostDtoall b = new()
        {
            id = p.id,
            cotent = p.cotent,
            date = p.date,
            img = p.img,
            shorttitle = p.shorttitle,
            title = p.title,
            urlhandler = p.urlhandler,
            isvisible = p.isvisible,
            Author = p.Author,
            Categories = p.Categories.Select(x=> new category()
            {
                id = x.id,
                name = x.name,
                urlhadle = x.urlhadle
            }).ToList()
        };
        return b;
    }
    [HttpPut]
    [Route("postblog/{id}")]
    public async ValueTask<IActionResult> updatepost(Guid id, BlogppostDto p)
    {
        Postblog pb = new()
        {
            id = id,
            cotent = p.cotent,
            date = p.date,
            img = p.img,
            shorttitle = p.shorttitle,
            title = p.title,
            urlhandler = p.urlhandler,
            isvisible = p.isvisible,
            Author = p.Author
        };
        _context.postbloguw.update(pb);
        _context.save();
        return Ok();
    }
     
}