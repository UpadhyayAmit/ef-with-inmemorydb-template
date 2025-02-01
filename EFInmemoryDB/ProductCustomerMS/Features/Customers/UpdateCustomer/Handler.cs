using ProductCustomerMS.Data;

namespace ProductCustomerMS.Features.Customers.UpdateCustomer
{
    public class Handler
    {
        private readonly AppDbContext _context;

        public Handler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Request request)
        {
            var customer = await _context.Customers.FindAsync(request.Id);
            if (customer == null) return false;

            customer.Name = request.Name;
            customer.Email = request.Email;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
