using ProductCustomerMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ProductCustomerMS.Tests
{
    public class CustomerEndpointsTests : IClassFixture<ApiWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CustomerEndpointsTests(ApiWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateCustomer_ShouldReturnCreated()
        {
            // Arrange
            var request = new Features.Customers.CreateCustomer.Request
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/customers", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetCustomer_ShouldReturnCustomer()
        {
            // Arrange
            var createRequest = new Features.Customers.CreateCustomer.Request
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com"
            };
            var createResponse = await _client.PostAsJsonAsync("/customers", createRequest);
            var customerId = await createResponse.Content.ReadFromJsonAsync<int>();

            // Act
            var response = await _client.GetAsync($"/customers/{customerId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var customer = await response.Content.ReadFromJsonAsync<Customer>();
            customer.Should().NotBeNull();
            customer.Name.Should().Be("Jane Doe");
        }

        [Fact]
        public async Task UpdateCustomer_ShouldReturnOk()
        {
            // Arrange
            var createRequest = new Features.Customers.CreateCustomer.Request
            {
                Name = "John Smith",
                Email = "john.smith@example.com"
            };
            var createResponse = await _client.PostAsJsonAsync("/customers", createRequest);
            var customerId = await createResponse.Content.ReadFromJsonAsync<int>();

            var updateRequest = new Features.Customers.UpdateCustomer.Request
            {
                Id = customerId,
                Name = "John Updated",
                Email = "john.updated@example.com"
            };

            // Act
            var response = await _client.PutAsJsonAsync("/customers", updateRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnOk()
        {
            // Arrange
            var createRequest = new Features.Customers.CreateCustomer.Request
            {
                Name = "Alice",
                Email = "alice@example.com"
            };
            var createResponse = await _client.PostAsJsonAsync("/customers", createRequest);
            var customerId = await createResponse.Content.ReadFromJsonAsync<int>();

           
            // Act
            var response = await _client.DeleteAsync($"/customers/{customerId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
