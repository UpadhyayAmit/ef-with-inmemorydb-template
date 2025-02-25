---
title: "Clean Architecture, Kafka, and gRPC integration in .NET Core Web API"
datePublished: Tue Feb 25 2025 13:38:47 GMT+0000 (Coordinated Universal Time)
cuid: cm7kj8ohz000509jy52x2ccow
slug: clean-architecture-kafka-and-grpc-integration-in-net-core-web-api
cover: https://cdn.hashnode.com/res/hashnode/image/upload/v1740490597131/f6a73cd3-4c34-492a-bc3f-1ac87e22540c.png
tags: microservices, backend, kafka, net-core, grpc, clean-architecture

---

To create a .NET Core API project that includes Clean Architecture, models for users and products, Kafka integration for event publishing and consumption, and gRPC server and client support, follow these steps:

## **Step 1: Set Up Clean Architecture Project**

1. **Create a new .NET Core Web API project** using Visual Studio or the command line:
    
    ```bash
    dotnet new webapi -n MyCleanApi
    ```
    
2. **Add layers for Clean Architecture**:
    
    * **Domain Layer**: Contains entities and business logic.
        
    * **Application Layer**: Contains application services and interfaces.
        
    * **Infrastructure Layer**: Handles data access and external services.
        
    * **Presentation Layer**: The main Web API project.
        

## **Step 2: Implement Domain Layer**

1. **Create a new class library for the Domain Layer**:
    
    ```bash
    dotnet new classlib -n MyCleanApi.Domain
    ```
    
2. **Add User and Product entities**:
    
    ```csharp
    csharp// MyCleanApi.Domain/Entities/User.cs
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
    
    // MyCleanApi.Domain/Entities/Product.cs
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    ```
    

## **Step 3: Implement Application Layer**

1. **Create a new class library for the Application Layer**:
    
    ```bash
    dotnet new classlib -n MyCleanApi.Application
    ```
    
2. **Add interfaces and services**:
    
    ```csharp
    csharp// MyCleanApi.Application/Services/IUserService.cs
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
    }
    
    // MyCleanApi.Application/Services/UserService.cs
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
    
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    
        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }
    }
    ```
    

## **Step 4: Implement Infrastructure Layer**

1. **Create a new class library for the Infrastructure Layer**:
    
    ```bash
    dotnet new classlib -n MyCleanApi.Infrastructure
    ```
    
2. **Add repositories and database context**:
    
    * Use Entity Framework Core for database operations.
        
    
    ```csharp
    csharp// MyCleanApi.Infrastructure/Repositories/IUserRepository.cs
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
    }
    
    // MyCleanApi.Infrastructure/Repositories/UserRepository.cs
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;
    
        public UserRepository(DbContext context)
        {
            _context = context;
        }
    
        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
    ```
    

## **Step 5: Kafka Integration**

1. **Install Kafka NuGet packages**:
    
    ```bash
    Install-Package Confluent.Kafka
    ```
    
2. **Create a Kafka producer and consumer**:
    
    ```csharp
    csharp// MyCleanApi.Infrastructure/Kafka/KafkaProducer.cs
    public class KafkaProducer
    {
        private readonly IProducer<string, string> _producer;
    
        public KafkaProducer(IProducer<string, string> producer)
        {
            _producer = producer;
        }
    
        public async Task ProduceAsync(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<string, string> { Value = message });
        }
    }
    
    // MyCleanApi.Infrastructure/Kafka/KafkaConsumer.cs
    public class KafkaConsumer : IHostedService
    {
        private readonly IConsumer<string, string> _consumer;
    
        public KafkaConsumer(IConsumer<string, string> consumer)
        {
            _consumer = consumer;
        }
    
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(new[] { "my-topic" });
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(cancellationToken);
                Console.WriteLine($"Received message: {result.Message.Value}");
            }
        }
    
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
    ```
    

## **Step 6: gRPC Server and Client**

1. **Create a new gRPC service**:
    
    ```csharp
    csharp// MyCleanApi.Application/Services/UserService.proto
    syntax = "proto3";
    
    package UserService;
    
    service UserService {
        rpc GetUser(GetUserRequest) returns (User) {}
    }
    
    message GetUserRequest {
        int32 id = 1;
    }
    
    message User {
        int32 id = 1;
        string name = 2;
    }
    ```
    
2. **Implement gRPC server**:
    
    ```csharp
    csharp// MyCleanApi.Application/Services/UserService.cs
    public class UserService : UserServiceBase
    {
        private readonly IUserRepository _userRepository;
    
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    
        public override async Task<User> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = await _userRepository.GetUserAsync(request.Id);
            return new User { Id = user.Id, Name = user.Name };
        }
    }
    ```
    
3. **Implement gRPC client**:
    
    ```csharp
    csharp// MyCleanApi.Application/Services/UserServiceClient.cs
    public class UserServiceClient
    {
        private readonly UserService.UserServiceClient _client;
    
        public UserServiceClient(UserService.UserServiceClient client)
        {
            _client = client;
        }
    
        public async Task<User> GetUserAsync(int id)
        {
            var request = new GetUserRequest { Id = id };
            return await _client.GetUserAsync(request);
        }
    }
    ```
    

## **Step 7: Configure Presentation Layer**

1. **Add references to other layers**:
    
    * In the main Web API project, add references to the Domain, Application, and Infrastructure layers.
        
2. **Configure services and controllers**:
    
    * Register services and repositories in `Startup.cs` or `Program.cs`.
        
    * Create controllers to handle HTTP requests.
        

## **Step 8: Run the Application**

1. **Run the Web API project**:
    
    ```bash
    dotnet run
    ```
    
2. **Test Kafka and gRPC functionality**:
    
    * Use tools like Postman or curl to test Web API endpoints.
        
    * Use Kafka tools to verify message publishing and consumption.
        
    * Use gRPC tools (like `grpcurl`) to test gRPC services.
        

## **Example Use Case**

* **Publishing an event to Kafka**:
    
    ```csharp
    csharp[ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly KafkaProducer _kafkaProducer;
    
        public UserController(KafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }
    
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            // Save user to database
            await _kafkaProducer.ProduceAsync("users", JsonConvert.SerializeObject(user));
            return Ok("User created");
        }
    }
    ```
    
* **Consuming Kafka events**:
    
    * The `KafkaConsumer` class will automatically consume messages from the "users" topic.
        
* **Using gRPC to retrieve a user**:
    
    ```csharp
    csharp[ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserServiceClient _userServiceClient;
    
        public UserController(UserServiceClient userServiceClient)
        {
            _userServiceClient = userServiceClient;
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userServiceClient.GetUserAsync(id);
            return Ok(user);
        }
    }
    ```
    

This setup integrates Clean Architecture, Kafka for event-driven architecture, and gRPC for efficient communication between services.