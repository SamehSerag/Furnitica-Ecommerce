using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageTestController : ControllerBase
    {
        public static IHostingEnvironment _environment;
        public ImageTestController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public class FIleUploadAPI
        {
            public string name { get; set; }
            public string age { get; set; }
        }
        [HttpPost]
        public async Task<string> Upload([FromForm] List<IFormFile> files,
           [FromForm] int id)
        {
            if (files.Count > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\images\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\images\\");
                    }
                    foreach (var file in files)
                    {
                        using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\images\\" + file.FileName))
                        {
                            file.CopyTo(filestream);
                            filestream.Flush();
                            //return "\\images\\" + file.FileName;
                        }
                    }
                    return "Added Successully" + id.ToString();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Unsuccessful";
            }

        }
    }
}
