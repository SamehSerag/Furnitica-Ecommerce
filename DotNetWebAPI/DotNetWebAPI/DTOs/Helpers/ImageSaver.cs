using AngularProject.Models;

namespace DotNetWebAPI.DTOs.Helpers
{
    public static class ImageSaver
    {
        public static List<Image> SaveLocally(List<IFormFile> imagesFiles, 
            IWebHostEnvironment environment, string folderPathInRoot = @"/images/product/")
        {
            List<Image> ImagesPaths = new List<Image>();   
            if (imagesFiles != null)
            {
                //var folderPath = @"/images/products/";
                string folderPathWithRoot = environment.WebRootPath + folderPathInRoot;

                if (!Directory.Exists(folderPathWithRoot))
                {
                    Directory.CreateDirectory(folderPathWithRoot);
                }

                foreach (var image in imagesFiles)
                {
                    using (FileStream fileStream = System.IO.File.Create(
                        folderPathWithRoot + image.FileName))
                    {
                        image.CopyTo(fileStream);
                        fileStream.Flush();
                        ImagesPaths.Add(new Image() 
                        { Src = folderPathInRoot + image.FileName });
                    }

                }
            }
            return ImagesPaths;
        }
    }
}
