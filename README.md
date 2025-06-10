# Order Management - DDD & WebAPI (Controllers)

[![.NET](https://img.shields.io/badge/.NET-9.0-purple)](https://dotnet.microsoft.com/)
[![ASP.NET](https://img.shields.io/badge/ASP.NET-9.0-blue)](https://dotnet.microsoft.com/ru-ru/apps/aspnet)
[![EFCore](https://img.shields.io/badge/EntityFrameworkCore-9.0.5-yellow)](https://learn.microsoft.com/ru-ru/ef/core/)
[![DDD](https://img.shields.io/badge/Architecture-DDD-blue)](https://domainlanguage.com/ddd/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

Order management system built with **Domain-Driven Design** principles and **ASP.NET Core WebAPI**.
Note: although the DDD pattern is used exclusively in **large projects**, I tried to implement it so that a small project doesn't look ultra cluttered.

## 📋 Overview
**The entire API is broken down into three understandable layers:**
- OrderManagement.Domain (class library project) --- this contains the basic business logic over the entities built for the subject area (order management in an online clothing shop). This layer is separated from the technical details, even though it contains a service for the API. As you can see, having primitive logic in the service does not mean that we are using technical details.
- OrderManagement.Infrastructure (class library project) --- here we implement the technical details, namely data manipulation (adding, deleting, retrieving, and we duplicate the SaveChangesAsync() function to save changes directly in the service). To be specific, this layer implements the repository interface that is defined in the Domain layer, the DbContext definition, and the storage of migrations.

**So why did I duplicate the SaveChangesAsync() function?** Since the aggregate in the Domain layer is the Order class, all changes concerning the OrderItems and addresses nested in it happen **ONLY THROUGH THEM**. In the service itself, we get an instance from the database and make changes via Order methods. But in the service we don't have direct access to the database context. Hence the answer to the question.
- OrderManagement.API (ASP.NET Core Web-API project) --- The main purpose of this layer is to coordinate tasks. Each client contacts our application directly through this layer. It contains a controller that defines the available methods, DTO classes to retrieve data and filter it through attributes and some other settings.

## ⚙️ Technologies
- **SDK**: .NET 9
- **Backend**: ASP.NET Core, Entity Framework Core
- **Architecture**: DDD
- **Database**: SQL Server (localDb for local testing). Note: add the database connection string to Secrets.json.
- **Tools**: Swagger (Swashbuckle)

## 🚀 Project features
1. Good commenting on the code. You will have no problems understanding why it is done this way.
2. Own exceptions to fully understand at what level the problem occurred and why it occurred.
3. Easy project structure.

**Regarding exceptions**. They are contained in OrderManagement.Domain\Exceptions. There are 3 types defined:
- DomainException.cs --- this exception is generated in domain model constructors as well as in ValueObject (ShippingAddress.cs). If it is generated, the data that comes in to create entities clearly contradicts business rules.
- OrderIdNotFoundException.cs and OrderItemIdNotFoundException.cs --- these exceptions are generated in the OrderService.cs service. The very name of the exceptions speaks for itself.

What is this even done for? As you can see, most instructions in controller methods are wrapped in try/catch. Based on the exception variants, the handler returns different responses that will be understandable to the client

## 🧩 Project Structure

```txt
├── OrderManagement.Domain
│   ├── Entities (Basic models built with the subject area in mind)
│   ├── Exceptions (Own exception classes)
│   ├── Repositories (A repository interface that defines the required minimum methods with the database)
│   ├── Services (The service interface and the service itself, which includes primitive logic limited to technical details)
│   ├── ValueObjects (Ordering address)
├── OrderManagement.API
│   ├── Common/Extensions (An extension method that configures ApiBehaviorOptions on exceptions in Middleware)
│   ├── Controllers 
│   ├── Models (DTOs)
│   └── ...other
├── OrderManagement.Infrastructure
│   ├── Data (DbContext)
│   ├── Migrations
│   ├── Repositories (Defining the repository interface )
```

## 🌟 API Endpoints
![image](https://github.com/user-attachments/assets/9c3eb9f5-a497-4688-b925-6d41f3693e24)

| Method | Endpoint                  | Description                |
|--------|---------------------------|----------------------------|
| `GET`  | `/api/Orders/{customerId}/all-orders` | Retrieving all orders by customer id |
| `GET`  | `/api/Orders/{orderId}/all-items` | Retrieving all OrderItem by order id |
| `POST` | `/api/Orders/order/{customerId}` | Adding a new order without OrderItems |
| `POST` | `/api/Orders/{orderId}/item` | Adding an OrderItem to an existing order |
| `PUT`  | `/api/Orders/{orderId}/address`       | Updating the address of an order |
| `DELETE` | `/api/Orders/{orderId}`     | Deleting an order and then deleting all OrderItems related to it (cascading deletion) |
| `DELETE` | `/api/Orders/{orderId}/item/{itemId}`     | Deleting an OrderItem from an existing order |

