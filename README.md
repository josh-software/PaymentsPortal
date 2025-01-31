# Payment Portal
Payment Portal is a basic account management system, designed as a learning project to demonstrate the use of .NET Core, React, TypeScript, and microservices using docker-compose.

## Key Technologies
- .NET 8
- React
- TypeScript
- Docker and Docker Compose
- RabbitMQ
- MassTransit
- MS SQL Server

## Goals:
  1. Support for different account types with the possibility for future expansion. For now, Current and Savings accounts are the only requirements.
  2. Ability to create and retrieve accounts.
  3. Ability to freeze(disable) an account.
  4. Written in C# (.NET 6 and onwards).

## Software Requirements:
- Docker Desktop
- .NET 8 SDK
- Node.js
- Visual Studio

## Getting Started:
1. Clone the repository.
2. Open the solution in Visual Studio.
3. Set the `docker-compose` project as the startup project.
4. Run the solution, the docker-compose project will start the services, open the swagger UI to test the API, and open the React app. All of this is debuggable in editor.

Warnings:
 - API calls from the React app will not work until the services are up and running. This may take a few seconds. If you experience issues, try refreshing the page.
 - RabbitMQ and SQL Server may take a while longer to start up, causing connection failures in the service logs. This should eventually resolve itself, but it not, try restarting the services.

### Accessing SQL Server:
- Server: `localhost, 5433`
- Username: `sa`
- Password: `Pass@word`

### Accessing RabbitMQ:
- URL: `http://localhost:15672`
- Username: `guest`
- Password: `guest`