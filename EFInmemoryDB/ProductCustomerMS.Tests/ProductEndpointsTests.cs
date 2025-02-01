using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductCustomerMS.Models;
using Xunit;

namespace ProductCustomerMS.Tests
{
    public class ProductEndpointsTests : IClassFixture<ApiWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductEndpointsTests(ApiWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnCreated()
        {
            // Arrange
            var customerRequest = new Features.Customers.CreateCustomer.Request
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
            };
            var customerResponse = await _client.PostAsJsonAsync("/customers", customerRequest);
            var customerId = await customerResponse.Content.ReadFromJsonAsync<int>();

            var productRequest = new Features.Products.CreateProduct.Request
            {
                Name = "Laptop",
                Price = 1200.00m,
                CustomerId = customerId,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/products", productRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetProduct_ShouldReturnProduct()
        {
            // Arrange
            var customerRequest = new Features.Customers.CreateCustomer.Request
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
            };
            var customerResponse = await _client.PostAsJsonAsync("/customers", customerRequest);
            var customerId = await customerResponse.Content.ReadFromJsonAsync<int>();

            var productRequest = new Features.Products.CreateProduct.Request
            {
                Name = "Smartphone",
                Price = 800.00m,
                CustomerId = customerId,
            };
            var productResponse = await _client.PostAsJsonAsync("/products", productRequest);
            var productId = await productResponse.Content.ReadFromJsonAsync<int>();

            // Act
            var response = await _client.GetAsync($"/products/{productId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var product = await response.Content.ReadFromJsonAsync<Product>();
            product.Should().NotBeNull();
            product.Name.Should().Be("Smartphone");
        }
    }
}
