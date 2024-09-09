# E-Commerce Application

This project is a full-stack E-Commerce application developed with .NET 8. It features a REST API backend and a .NET 8 MVC frontend, providing a robust and secure platform for online shopping. The application integrates several advanced features, including payment processing, messaging, and API management.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
  - [Running the Application](#running-the-application)
- [Architecture](#architecture)
- [Security](#security)
- [Payment Integration](#payment-integration)
- [Messaging Integration](#messaging-integration)
- [API Gateway](#api-gateway)
- [Contributing](#contributing)
- [License](#license)

## Features
- **Backend:** Developed using .NET 8 REST APIs to handle all server-side logic.
- **Frontend:** Built with .NET 8 MVC for dynamic and responsive user interfaces.
- **Data Storage:** SQL Server is used for persistent data storage.
- **Data Access:** Entity Framework (EF) is used for data access with a Code First approach.
- **Security:** Secured API endpoints using JWT with role-based authorization provided by .NET Identity.
- **Payment Integration:** Stripe payment gateway integrated with support for coupon functionality.
- **API Gateway:** Implemented using Ocelot for managing multiple API services.
- **Messaging:** Integrated Azure Service Bus for messaging, supporting both queues and topics.

## Technologies Used
- **Backend:** .NET 8, ASP.NET Core REST APIs
- **Frontend:** .NET 8 MVC
- **Database:** SQL Server
- **ORM:** Entity Framework Core (EF Core)
- **Authentication & Authorization:** JWT, .NET Identity
- **Payment Gateway:** Stripe
- **API Gateway:** Ocelot
- **Messaging:** Azure Service Bus (Queue and Topic)

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Azure Account](https://azure.microsoft.com/en-us/free/) (for Service Bus integration)
- [Stripe Account](https://stripe.com/) (for payment integration)

### Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/ecommerce-app.git
   cd ecommerce-app
