using BLG.ApplicationConract.uploadimages;
using BLG.Domin.uploadImage;
using BLG.Infrastructure.customRepository;
using Microsoft.AspNetCore.Mvc;

namespace Blog_WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class imageController : ControllerBase
{
    private readonly IUploadimageReposetory _uploadimageReposetory;

    public imageController(IUploadimageReposetory uploadimageReposetory)
    {
        _uploadimageReposetory = uploadimageReposetory;
    }

    [HttpPost]
    [Route("uploadimage")]
    public async Task<IActionResult> uploaded([FromForm] IFormFile file, [FromForm] string tiltle,
        [FromForm] string filename
    )
    {
        validation(file);
        uploadimg upimg = new()
        {
            fileExtension = Path.GetExtension(file.FileName).ToLower(),
            filename = filename,
            tiltle = tiltle,
            time = DateTime.Now,
        };
        var c = await _uploadimageReposetory.upload(file, upimg);
        UploadimageDto ui = new()
        {
            id = c.id,
            filename = c.filename,
            tiltle = c.tiltle,
            fileExtension = c.fileExtension,
            time = c.time,
            urlhandle = c.urlhandle
        };
        return Ok(ui);
    }

    private void validation(IFormFile file)
    {
        var format = new string[] { "png", "jpg", "jpeg" };
        if (!format.Contains(Path.GetExtension(file.FileName).ToLower()))
        {
            ModelState.AddModelError("file", "format unsuported");
        }
    }
}