using AngularProject.Models;
using AutoMapper;

namespace AngularAPI.Dtos.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product,
            ProductToReturnDto, ICollection<string>>
    {
        public IConfiguration Configuration { get; }

        public ProductUrlResolver(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }


        public ICollection<string> Resolve(Product source, ProductToReturnDto destination,
            ICollection<string> destMember, ResolutionContext context)
        {
            List<string> result = new List<string>();
            if(source.Images != null)
                foreach (var item in source.Images)
                {
                    if(!string.IsNullOrEmpty(item.Src) &&
                        !item.Src.Contains("https://") && 
                        !item.Src.Contains("http://"))
                    {

                        result.Add(Configuration["ApiUrl"] + item.Src);
                    }
                }

            return result;
        }
    }
}
