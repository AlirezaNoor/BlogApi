using BLG.Domin.uploadImage;
using Microsoft.AspNetCore.Http;

namespace BLG.Infrastructure.customRepository;

public interface IUploadimageReposetory
{
   Task<uploadimg> upload(IFormFile file,uploadimg upload);
}