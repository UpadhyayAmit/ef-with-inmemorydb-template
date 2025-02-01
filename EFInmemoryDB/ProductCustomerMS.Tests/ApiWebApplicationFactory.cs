using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCustomerMS.Data;

namespace ProductCustomerMS.Tests
{
    public class ApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                // Replace the database with an in-memory database for testing
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Necessary for supporting top-level statements
            builder.ConfigureHostConfiguration(config => {
                // Add any additional host configuration here
            });

            return base.CreateHost(builder);
        }

        public partial class Program { }
    }
}