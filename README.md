# Mango-Microservices-e-Commerce
<br/>

A robust, full-stack E-Commerce application built using microservices architecture with .NET 8.

🖼️ **Samples**


![image](https://github.com/user-attachments/assets/6cdb2a67-6be3-40bc-9b91-40c55a30da11)
![image](https://github.com/user-attachments/assets/e53fac4b-07a2-44b4-9968-43152c6b61c4)
![image](https://github.com/user-attachments/assets/c44d9893-867b-4f5b-b3b4-637ff587fc11)
![image](https://github.com/user-attachments/assets/ce538890-2b25-4c90-81b0-49c939d291ee)
![image](https://github.com/user-attachments/assets/8590a332-a0b4-4100-aca3-11508146657d)
![image](https://github.com/user-attachments/assets/8a58af6d-3147-4de4-9766-b76ee72abc6c)
![image](https://github.com/user-attachments/assets/38290327-79c7-424e-bb99-2973a4779e59)
![image](https://github.com/user-attachments/assets/ff97123a-f22f-4d43-954b-24892d27495f)






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
Copy git clone https://github.com/tahaet/Mango-Microservices-e-Commerce.git

Navigate to the project directory
Set up your SQL Server and update connection strings
Configure Azure Service Bus connection
Set up Stripe API keys
Run the individual microservices and the MVC frontend
