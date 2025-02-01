namespace ProductCustomerMS.Features.Products.UpdateProduct
{
    public class Request
    {
        public int Id { get; set; } = default!;
        public string? Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public int CustomerId { get; set; } = default!;
    }
}
