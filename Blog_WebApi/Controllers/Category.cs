using BLG.ApplicationConract.category;
using BLG.Domin.CategoryBlogAgg;
using BLG.Services.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class Category : ControllerBase
{
    private readonly Iunitofwork _context;

    public Category(Iunitofwork context)
    {
        _context = context;
    }

    //createCategory
    [HttpPost]
    [Authorize(Roles = "Writer")]
    [Route("Categories")]
    public async Task<CreateCategory> Create(CreateCategory e)
    {
        category c = new()
        {
            name = e.name,
            urlhadle = e.urlhadle
        };
        _context.categoryuw.insert(c);
        _context.save();
        return e;
    }

    [HttpGet]
    [Route("categoriesList")]
    public async ValueTask<IEnumerable<CategoriesList>> Getall()
    {
        var categories = _context.categoryuw.get().ToList();
        List<CategoriesList> lst = new();
        foreach (var i in categories)
        {
            lst.Add(new CategoriesList()
            {
                id = i.id,
                name = i.name,
                urlhadle = i.urlhadle
            });
        }

        return lst;
    }

    [HttpGet]
    [Route("category/{id:guid}")]
    public async ValueTask<IActionResult> getbyid([FromRoute] Guid id)
    {
        var category = _context.categoryuw.Getbyid(id);
        if (category == null)
        {
            return NoContent();
        }

        CategoriesList c = new()
        {
            id = category.id,
            name = category.name,
            urlhadle = category.urlhadle
        };
        return Ok(c);
    }

    [HttpPut]
    [Authorize(Roles = "Writer")]
    [Route("catregoryedit/{id:guid}")]
    public async ValueTask<IActionResult> updatecategory([FromRoute] Guid id, CategoriesList c)
    {
        try
        {
            category cat = new()
            {
                id = id,
                name = c.name,
                urlhadle = c.urlhadle
            };
            _context.categoryuw.update(cat);
            _context.save();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e);
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Writer")]
    [Route("catregoryedit/{id:guid}")]
    public async ValueTask<IActionResult> deletecategory([FromRoute]Guid id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        _context.categoryuw.deletebyid(id);
        _context.save();
        return Ok();
    }
}
