using Microsoft.AspNetCore.Mvc;

namespace ProductCustomerMS.Features.Customers.GetCustomer
{
    public static class Endpoint
    {
        public static async Task<IResult> GetCustomer(int id, Handler handler)
        {
            var customer = await handler.Handle(new Request { Id = id });
            return customer == null ? Results.NotFound() : Results.Ok(customer);
        }
    }
}
