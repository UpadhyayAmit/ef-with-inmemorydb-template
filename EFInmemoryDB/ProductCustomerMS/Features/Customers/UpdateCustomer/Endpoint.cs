using Microsoft.AspNetCore.Mvc;

namespace ProductCustomerMS.Features.Customers.UpdateCustomer
{
    public static class Endpoint
    {
        public static async Task<IResult> UpdateCustomer([FromBody] Request request, Handler handler)
        {
            var success = await handler.Handle(request);
            return success ? Results.Ok() : Results.NotFound();
        }
    }
}
