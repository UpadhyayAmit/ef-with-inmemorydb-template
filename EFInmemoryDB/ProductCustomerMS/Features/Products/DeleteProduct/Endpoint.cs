using Microsoft.AspNetCore.Mvc;

namespace ProductCustomerMS.Features.Products.DeleteProduct
{
    public static class Endpoint
    {
        public static async Task<IResult> DeleteProduct([FromBody] int id, Handler handler)
        {
            var success = await handler.Handle(new Request { Id = id });
            return success ? Results.Ok() : Results.NotFound();
        }
    }
}
