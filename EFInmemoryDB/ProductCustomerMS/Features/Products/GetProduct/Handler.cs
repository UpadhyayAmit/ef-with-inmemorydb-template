using Microsoft.EntityFrameworkCore;
using ProductCustomerMS.Data;
using ProductCustomerMS.Models;
using System.Reflection.Metadata.Ecma335;


namespace ProductCustomerMS.Features.Products.GetProduct
{
    public class Handler(AppDbContext appDbContext)
    {
        public async Task<Product?> Handle(Request request)
        {
           return await appDbContext.Products.Include(p => p.Customer).FirstOrDefaultAsync(p => p.Id == request.Id);
        }
    }
}
