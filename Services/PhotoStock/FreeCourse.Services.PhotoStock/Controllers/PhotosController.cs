using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.CustomBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        //cancellationToken kullnaıcı  indirmeye başladıgında sayfayı kapatırse indirme işlemini durudur
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null &&  photo.Length>0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","photos", photo.FileName);


                using var stream = new FileStream(path, FileMode.Create);

                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = "photos/"+photo.FileName;

                PhotoDto photoDto = new() { PhotoUrl = returnPath };

                return CreateActionResultInstance(ResponseDto<PhotoDto>.Success(photoDto, 200));

            }

            return CreateActionResultInstance(ResponseDto<PhotoDto>.Fail("photo is emty", 400));

        }

        [HttpGet]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(ResponseDto<NoContent>.Fail("fotograf bulunamadı", 404));
            }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(ResponseDto<NoContent>.Success(204));
        }


    }
}
