namespace AngularAPI.Dtos
{
    public class OrderProductDto
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
