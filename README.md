Full-Stack E-Commerce Application
🛍️ Overview
This project is a comprehensive E-Commerce application built using modern technologies and best practices. It features a robust backend API, a user-friendly frontend, secure authentication, payment processing, and cloud messaging integration.
✨ Features

Full-stack application with .NET 8 REST APIs (backend) and .NET 8 MVC (frontend)
SQL Server for data storage
Entity Framework Core for data access using Code First approach
JWT-based authentication with role-based authorization using .NET Identity
Cart and Orders management
Stripe payment integration with coupon functionality
API Gateway implementation using Ocelot
Azure Service Bus messaging integration (queue and topic)

🛠️ Tech Stack
ComponentTechnologyBackend.NET 8 REST APIsFrontend.NET 8 MVCDatabaseSQL ServerORMEntity Framework CoreAuthenticationJWT, .NET IdentityPayment ProcessingStripeAPI GatewayOcelotCloud MessagingAzure Service Bus
🚀 Getting Started
bashCopy# Clone the repository
git clone https://github.com/yourusername/ecommerce-app.git

# Navigate to the project directory
cd ecommerce-app

# Install dependencies
dotnet restore

# Run the application
dotnet run
🔗 API Endpoints

GET /api/products: Retrieve all products
POST /api/orders: Create a new order
GET /api/cart: Retrieve user's cart
... (add more endpoints as needed)

🖥️ Frontend Pages

Home Page: /
Product Catalog: /products
Shopping Cart: /cart
Checkout: /checkout
... (add more pages as needed)

💾 Database Schema
mermaidCopyerDiagram
    CUSTOMER ||--o{ ORDER : places
    CUSTOMER {
        string id
        string name
        string email
    }
    ORDER ||--|{ ORDER_ITEM : contains
    ORDER {
        int id
        date created_at
        string status
    }
    PRODUCT ||--o{ ORDER_ITEM : "ordered in"
    PRODUCT {
        int id
        string name
        float price
    }
🔐 Authentication and Authorization
This application uses JWT (JSON Web Tokens) for authentication and implements role-based authorization using .NET Identity. This ensures secure access to API endpoints based on user roles.
💳 Payment Processing
Payment processing is handled through Stripe integration, which includes support for coupon functionality. This allows for flexible pricing and promotional offers within the E-Commerce platform.
🌉 API Gateway
The application implements an API Gateway using Ocelot, providing a unified entry point for API requests and enabling features like:

Request aggregation
Routing
Load balancing

☁️ Cloud Messaging
Azure Service Bus is integrated for messaging, utilizing both queue and topic functionalities. This enables robust, scalable communication between different components of the application.
🤝 Contributing
We welcome contributions to this project! Please follow these steps:

Fork the repository
Create a new branch: git checkout -b feature-branch-name
Make your changes and commit them: git commit -m 'Add some feature'
Push to the branch: git push origin feature-branch-name
Submit a pull request
