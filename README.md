# Mango-Microservices-e-Commerce
<br/>

A robust, full-stack E-Commerce application built using microservices architecture with .NET 8.


🚀 Features

Full stack E-Commerce app with .NET 8 REST APIs for backend and .NET 8 MVC for frontend
Microservices architecture for scalability and modularity
SQL Server for data storage with Entity Framework (EF) for data access using code-first approach
Secure API endpoints using JWT with role-based authorization via .NET Identity
Stripe payment integration with coupon functionality
API Gateway implementation using Ocelot
Azure Service Bus messaging integration (queue and topic)

🛠️ Tech Stack

Backend: .NET 8 REST APIs
Frontend: .NET 8 MVC
Database: SQL Server
ORM: Entity Framework Core
Authentication: JWT, .NET Identity
Payment Processing: Stripe
API Gateway: Ocelot
Message Broker: Azure Service Bus

📦 Microservices

API Gateway (Ocelot): Acts as a reverse proxy, routing external client requests to the appropriate microservices.
Azure Service Bus (Queue & Topic): Facilitates asynchronous communication between microservices via message brokering.
Auth API Service: Manages user authentication and authorization.
Coupon API Service: Handles coupon-related functionalities.
Product API Service: Manages product-related operations.
Shopping Cart API Service: Oversees user shopping cart operations.
Order API Service: Manages order processing and fulfillment.
Email API Service: Sends email notifications to users.
Payment Gateway: Processes payment transactions.
Reward API Service: Manages user rewards.


🚀 Getting Started

Clone the repository
Copygit clone https://github.com/tahaet/Mango-Microservices-e-Commerce.git

Navigate to the project directory
Set up your SQL Server and update connection strings
Configure Azure Service Bus connection
Set up Stripe API keys
Run the individual microservices and the MVC frontend
