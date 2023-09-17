using BLG.ApplicationConract.category;
using BLG.Domin.CategoryBlogAgg;
using BLG.Services.UnitOfWork;
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
    public async ValueTask<IActionResult> getbyid([FromRoute]Guid id)
    {
        var category = _context.categoryuw.Getbyid(id);
        if (category==null)
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
}