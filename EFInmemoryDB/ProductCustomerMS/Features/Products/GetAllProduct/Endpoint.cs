using Microsoft.AspNetCore.Mvc;
namespace ProductCustomerMS.Features.Products.GetAllProduct
{
    public static class Endpoint
    {
        public static async Task<IResult> GetAllProducts(Handler handler)
        {
            var products = await handler.Handle();
            return Results.Ok(products);
        }
    }
}
