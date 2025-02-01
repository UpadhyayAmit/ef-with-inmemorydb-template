using ProductCustomerMS.Data;
using ProductCustomerMS.Models;


namespace ProductCustomerMS.Features.Products.CreateProduct
{
    public class Handler(AppDbContext appDbContext)
    {
        public async Task<int> Handle(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                CustomerId = request.CustomerId
            };

            appDbContext.Products.Add(product);
            await appDbContext.SaveChangesAsync();

            return product.Id;
        }
    }
}
