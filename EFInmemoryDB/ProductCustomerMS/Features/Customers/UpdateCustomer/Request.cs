namespace ProductCustomerMS.Features.Customers.UpdateCustomer
{
    public class Request
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
