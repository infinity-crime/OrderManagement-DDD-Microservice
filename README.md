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
- OrderManagement.Domain --- this contains the basic business logic over the entities built for the subject area (order management in an online clothing shop). This layer is separated from the technical details, even though it contains a service for the API. As you can see, having primitive logic in the service does not mean that we are using technical details.
