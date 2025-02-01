using Microsoft.EntityFrameworkCore;
using ProductCustomerMS.Data;
using ProductCustomerMS.Models;


namespace ProductCustomerMS.Features.Products.DeleteProduct
{
    public class Handler(AppDbContext appDbContext)
    {
        public async Task<bool> Handle(Request request)
        {
            var product = await appDbContext.Products.FindAsync(request.Id);
            if (product == null) return false;

            appDbContext.Products.Remove(product);
            await appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
