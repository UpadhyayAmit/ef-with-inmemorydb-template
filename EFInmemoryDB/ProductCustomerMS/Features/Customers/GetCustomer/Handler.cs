using Microsoft.EntityFrameworkCore;
using ProductCustomerMS.Data;
using ProductCustomerMS.Models;

namespace ProductCustomerMS.Features.Customers.GetCustomer
{
    public class Handler
    {
        private readonly AppDbContext _context;

        public Handler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> Handle(Request request)
        {
            return await _context.Customers               
                .FirstOrDefaultAsync(c => c.Id == request.Id);
        }
    }
}
