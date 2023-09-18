using BLG.ApplicationConract.Blog_post;
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
            Author = p.Author
        };
        _context.postbloguw.insert(post);
        _context.save();
        return Ok();
    }
}