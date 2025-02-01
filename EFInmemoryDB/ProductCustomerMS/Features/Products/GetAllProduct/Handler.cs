using Microsoft.EntityFrameworkCore;
using ProductCustomerMS.Data;
using ProductCustomerMS.Models;
using System.Reflection.Metadata.Ecma335;


namespace ProductCustomerMS.Features.Products.GetAllProduct
{
    public class Handler(AppDbContext appDbContext)
    {
        public async Task<List<Product>> Handle()
        {
            return await appDbContext.Products
                 .Include(p => p.Customer) // Include related customer
                 .ToListAsync();
        }
    }
}
