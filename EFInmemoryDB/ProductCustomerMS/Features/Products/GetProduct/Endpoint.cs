using Microsoft.AspNetCore.Mvc;
namespace ProductCustomerMS.Features.Products.GetProduct
{
    public static class Endpoint
    {
        public static async Task<IResult> GetProduct(int id, Handler handler)
        {
            var product = await handler.Handle(new Request { Id = id });
            return product is null ? Results.NotFound() : Results.Ok(product);
        }
    }
}
