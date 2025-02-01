using Microsoft.AspNetCore.Mvc;

namespace ProductCustomerMS.Features.Products.UpdateProduct
{
    public static class Endpoint
    {
        public static async Task<IResult> UpdateProduct([FromBody] Request request, Handler handler)
        {
            var success = await handler.Handle(request);
            return success ? Results.Ok() : Results.NotFound();
        }
    }
}
