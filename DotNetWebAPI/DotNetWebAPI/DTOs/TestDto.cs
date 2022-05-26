namespace DotNetWebAPI.DTOs
{
    public class TestDto
    {
        //public string name { get; set; }
        public int Price { get; set; }
        public int? MyPropertyNull { get; set; }
        public string? name { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
