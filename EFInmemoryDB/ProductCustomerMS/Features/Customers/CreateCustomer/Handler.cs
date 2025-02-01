using ProductCustomerMS.Data;
using ProductCustomerMS.Models;

namespace ProductCustomerMS.Features.Customers.CreateCustomer
{
    public class Handler
    {
        private readonly AppDbContext _context;

        public Handler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(Request request)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer.Id;
        }
    }
}
