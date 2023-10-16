using BLG.ApplicationConract.Authoration;
using BLG.Services.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog_WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AthuController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly Iunitofwork _context;

    public AthuController(UserManager<IdentityUser> userManager, Iunitofwork context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost]
    [Route("Athu")]
    public async Task<IActionResult> Register(RegisterDto d)
    {
        var user = new IdentityUser()
        {
            Email = d.name.Trim(),
            UserName = d.name.Trim(),
        };
        var result = await _userManager.CreateAsync(user, d.password);
        if (result.Succeeded)
        {
            var resultRole = await _userManager.AddToRoleAsync(user, "Reader");
            if (resultRole.Succeeded)
            {
                return Ok();
            }
        }
        else
        {
            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }

        return ValidationProblem(ModelState);
    }
}