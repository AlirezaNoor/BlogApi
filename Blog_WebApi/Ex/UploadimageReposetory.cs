using BLG.Domin.uploadImage;
using BLG.Infrastructure.Context;
using BLG.Infrastructure.customRepository;
using Microsoft.AspNetCore.Http;

namespace BLG.Services.Customrepository;

public class UploadimageReposetory:IUploadimageReposetory
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IWebHostEnvironment _environment;
    private readonly ApplicationContext _context;
    public UploadimageReposetory(IHttpContextAccessor contextAccessor,IWebHostEnvironment host, ApplicationContext context)
    {
        _contextAccessor = contextAccessor;
        _environment = host;
        _context = context;
    }

    public async Task<uploadimg> upload(IFormFile file,uploadimg upload)
    {
        var local = Path.Combine(_environment.ContentRootPath, "images", $"{upload.filename}{upload.fileExtension}");
        using var system = new FileStream(local, FileMode.Create);
        await file.CopyToAsync(system);
        var httprequest = _contextAccessor.HttpContext.Request;
        var urlpath =
            $"{httprequest.Scheme}://{httprequest.Host}{httprequest.PathBase}/images/{upload.filename}{upload.fileExtension}";
        upload.urlhandle = urlpath;
        await _context.uploadimage.AddAsync(upload);
        await _context.SaveChangesAsync();
        return upload;
    }
}