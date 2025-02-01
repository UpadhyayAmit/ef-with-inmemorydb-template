using Microsoft.AspNetCore.Mvc;

namespace ProductCustomerMS.Features.Customers.CreateCustomer
{
    public static class Endpoint
    {
        public static async Task<IResult> CreateCustomer([FromBody] Request request, Handler handler)
        {
            var customerId = await handler.Handle(request);
            return Results.Created($"/customers/{customerId}", customerId);
        }
    }



}


