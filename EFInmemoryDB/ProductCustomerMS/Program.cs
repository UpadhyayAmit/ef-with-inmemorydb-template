using Microsoft.EntityFrameworkCore;
using ProductCustomerMS.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductCustomerDb"));

// Register the Handler as a service
builder.Services.AddScoped<ProductCustomerMS.Features.Customers.CreateCustomer.Handler>();
builder.Services.AddScoped<ProductCustomerMS.Features.Customers.GetCustomer.Handler>();
builder.Services.AddScoped<ProductCustomerMS.Features.Customers.UpdateCustomer.Handler>();
builder.Services.AddScoped<ProductCustomerMS.Features.Customers.DeleteCustomer.Handler>();


builder.Services.AddScoped<ProductCustomerMS.Features.Products.CreateProduct.Handler>();
builder.Services.AddScoped<ProductCustomerMS.Features.Products.GetProduct.Handler>();
builder.Services.AddScoped<ProductCustomerMS.Features.Products.GetAllProduct.Handler>();
builder.Services.AddScoped<ProductCustomerMS.Features.Products.UpdateProduct.Handler>();
builder.Services.AddScoped<ProductCustomerMS.Features.Products.DeleteProduct.Handler>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName); // Use full type name as schema ID
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();


app.MapPost("/customers", ProductCustomerMS.Features.Customers.CreateCustomer.Endpoint.CreateCustomer);
app.MapGet("/customers/{id}", ProductCustomerMS.Features.Customers.GetCustomer.Endpoint.GetCustomer);
app.MapPut("/customers", ProductCustomerMS.Features.Customers.UpdateCustomer.Endpoint.UpdateCustomer);
app.MapDelete("/customers/{id}", ProductCustomerMS.Features.Customers.DeleteCustomer.Endpoint.DeleteCustomer);


app.MapPost("/products", ProductCustomerMS.Features.Products.CreateProduct.Endpoint.CreateProduct);
app.MapGet("/products/{id}", ProductCustomerMS.Features.Products.GetProduct.Endpoint.GetProduct);
app.MapGet("/products", ProductCustomerMS.Features.Products.GetAllProduct.Endpoint.GetAllProducts);
app.MapPut("/products", ProductCustomerMS.Features.Products.UpdateProduct.Endpoint.UpdateProduct);
app.MapDelete("/products/{id}", ProductCustomerMS.Features.Products.DeleteProduct.Endpoint.DeleteProduct);

app.MapControllers();

app.Run();

public partial class Program { } // Add this at the bottom