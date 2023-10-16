using BLG.ApplicationConract.Authoration;
using BLG.Infrastructure.customRepository;
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
    private readonly ITokenReposetory _tokenReposetory;

    public AthuController(UserManager<IdentityUser> userManager, Iunitofwork context ,ITokenReposetory tokenReposetory)
    {
        _userManager = userManager;
        _context = context;
        _tokenReposetory = tokenReposetory;
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

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody]LoginDto ld)
    {
        var user = await _userManager.FindByEmailAsync(ld.Email);
        if (user!=null)
        {
            var checkpassword = await _userManager.CheckPasswordAsync(user, ld.password);
            if (checkpassword)
            {
                var role = await _userManager.GetRolesAsync(user);
                ResponseDto res = new ResponseDto()
                {
                    Email = ld.Email,
                    Token = _tokenReposetory.createjwt(user,role.ToList()),
                    role = role.ToList()
                };
                return Ok(res);
            }
            
        }
        else
        {
        ModelState.AddModelError("","user or email not correct!");
        }

        return ValidationProblem(ModelState);
    }
}