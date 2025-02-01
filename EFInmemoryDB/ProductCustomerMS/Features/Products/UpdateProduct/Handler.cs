using Microsoft.EntityFrameworkCore;
using ProductCustomerMS.Data;
using ProductCustomerMS.Models;


namespace ProductCustomerMS.Features.Products.UpdateProduct
{
    public class Handler(AppDbContext appDbContext)
    {
        public async Task<bool> Handle(Request request)
        {
            var product = await appDbContext.Products.FindAsync(request.Id);
            if (product == null) return false;

            product.Name = request.Name;
            product.Price = request.Price;
            product.CustomerId = request.CustomerId;

            await appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
