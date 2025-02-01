namespace ProductCustomerMS.Features.Products.CreateProduct
{
    public class Request
    {
        public string?  Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public int CustomerId { get; set; } = default!;
    }
}
