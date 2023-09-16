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
}