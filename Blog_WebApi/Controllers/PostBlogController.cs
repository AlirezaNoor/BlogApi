using BLG.ApplicationConract.Blog_post;
using BLG.Domin.CategoryBlogAgg;
using BLG.Domin.PostBlogAgg;
using BLG.Infrastructure.customRepository;
using BLG.Services.Extentions;
using BLG.Services.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class PostBlogController : ControllerBase
{
    private readonly Iunitofwork _context;
    private readonly IPostblogReposetory _postblogReposetory;

    public PostBlogController(Iunitofwork context, IPostblogReposetory postblogReposetory)
    {
        _context = context;
        _postblogReposetory = postblogReposetory;
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

    [Authorize]
    [HttpGet]
    [Route("postblog")]
    public async Task<IEnumerable<BlogppostDtoall>> getAll()
    {
        var post = _context.postbloguw.get(null, "Categories");
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
                Categories = p.Categories.Select(x => new category
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
    public async Task<BlogppostDtoall> get(Guid id)
    {
        var p = _context.postbloguw.get(x => x.id == id, "Categories").FirstOrDefault();
        try
        {
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
                Categories = p.Categories.Select(x => new category()
                {
                    id = x.id,
                    name = x.name,
                    urlhadle = x.urlhadle
                }).ToList()
            };
            return b;
        }
        catch (Exception e)
        {
            Logger.WriteToConsole(e, "postblog/{id}", "road by alireza", "this log in paostblog conteroll", 116);
        }

        return null;
    }

    [HttpPut]
    [Route("postblogedit/{id}")]
    public async Task<IActionResult> updatepost([FromRoute] Guid id, BlogppostDto p)
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
            Author = p.Author,
            Categories = new List<category>()
        };
        foreach (var i in p.category)
        {
            var mycategory = _context.categoryuw.Getbyid(i);
            pb.Categories.Add(mycategory);
        }

        _postblogReposetory.updatepostblog(pb);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] Guid id)
    {
        var post = _context.postbloguw.Getbyid(id);
        if (post == null)
        {
            return BadRequest();
        }

        _context.postbloguw.Delete(post);
        _context.save();
        return Ok();
    }

    [HttpGet]
    [Route("dtails/{Urlhandler}")]
    public BlogppostDtoall getbyurl([FromRoute] string Urlhandler)
    {
        var p = _context.postbloguw.get(x => x.urlhandler == Urlhandler, "Categories").FirstOrDefault();
        if (p == null)
        {
            return null;
        }

        BlogppostDtoall blog = new()
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
            Categories = p.Categories.Select(x => new category()
            {
                id = x.id,
                name = x.name,
                urlhadle = x.urlhadle
            }).ToList()
        };
        return blog;
    }
}