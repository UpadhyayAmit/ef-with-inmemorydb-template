# ef-with-inmemorydb-template

Entity Framework with in-memory database

## Tech Stack

- Docker
- .NET Core
- Entity Framework Core
- In-Memory Database
- Swagger
- Vertical Slice Architecture

## Overview

This template demonstrates how to create a .NET Core API using Entity Framework Core with an in-memory database. It includes Swagger for API documentation and follows the Vertical Slice Architecture pattern.

## Features

- **Entity Framework Core**: Used for data access with an in-memory database for simplicity and testing.
- **Swagger**: Integrated for API documentation and testing.
- **Vertical Slice Architecture**: Organizes code by features, promoting modularity and maintainability.

## Models

### Customer

```csharp
public class Customer
{
      public int Id { get; set; }
      public string Name { get; set; }
      public string Email { get; set; }
}
```

### Product

```csharp
public class Product
{
      public int Id { get; set; }
      public string Name { get; set; }
      public decimal Price { get; set; }
}
```

## Setup Instructions

1. **Clone the repository**:

   ```sh
   git clone https://github.com/upadhyayamit/ef-with-inmemorydb-template.git
   ```

2. **Navigate to the project directory**:

   ```sh
   cd ef-with-inmemorydb-template
   ```

3. **Run the application**:

   ```sh
   dotnet run
   ```

4. **Access Swagger UI**:
   Open your browser and navigate to `https://localhost:32771/swagger/index.html`.

## Usage

- Use the Swagger UI to interact with the API endpoints for Customer and Product models.
- The in-memory database will reset each time the application is restarted, making it ideal for testing and development.

## Contributing

Contributions are welcome! Please submit a pull request or open an issue to discuss any changes.

## License

This project is licensed under the MIT License.
