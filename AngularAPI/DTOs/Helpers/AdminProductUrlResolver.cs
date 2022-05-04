using AngularProject.Models;
using AutoMapper;

namespace AngularAPI.Dtos.Helpers
{
    public class AdminProductUrlResolver : IValueResolver<Product, AdminProductDto,
        ICollection<ImageDto>>
    {
        public IConfiguration Configuration { get; }

        public AdminProductUrlResolver(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public ICollection<ImageDto>? Resolve(Product source, 
            AdminProductDto destination, ICollection<ImageDto> destMember,
            ResolutionContext context)
        {
            List<ImageDto> newImages = new List<ImageDto>();  
            if(source.Images != null)
            {
                foreach (var image in source.Images)
                {
                    if (!string.IsNullOrEmpty(image.Src) &&
                        !image.Src.Contains("https://") && !image.Src.Contains("http://"))
                    {
                        ImageDto img = new ImageDto();
                        img.Id = image.Id;
                        img.Src = Configuration["ApiUrl"] + image.Src;
                        newImages.Add(img);
                    }
                }
                return newImages;
            }
            return null;

        }


    }
}
