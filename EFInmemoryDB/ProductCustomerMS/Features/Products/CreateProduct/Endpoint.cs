using Microsoft.AspNetCore.Mvc;

namespace ProductCustomerMS.Features.Products.CreateProduct
{
    public static class Endpoint
    {
        public static async Task<IResult> CreateProduct([FromBody] Request request, Handler handler)
        {
            var productId = await handler.Handle(request);
            return Results.Created($"/products/{productId}", productId);
        }
    }
}
