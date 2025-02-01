using Microsoft.AspNetCore.Mvc;

namespace ProductCustomerMS.Features.Customers.DeleteCustomer
{
    public static class Endpoint
    {
        public static async Task<IResult> DeleteCustomer( int id, Handler handler)
        {
            var result = await handler.Handle(new Request { Id = id });
            return result ? Results.NoContent() : Results.NotFound();
        }
    }
}
