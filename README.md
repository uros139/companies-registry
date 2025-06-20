# Companies Registry

This project is built with .NET 9 and Angular 17. While it includes a frontend, the main focus is on the backend.

It follows clean code principles using Domain-Driven Design (DDD) and Clean Architecture to promote maintainability and scalability.

The app provides simple CRUD use cases, but the main focus is on best practices, architectural structure, and configuring the project like a real-world system to support future growth.

## To run the app

### Prerequisites
- Docker and Docker Compose
- .NET 9 SDK (for running tests locally)

### Running the Application

To run the entire application locally using Docker:

```bash
docker compose up
```

The frontend application will be available at: http://localhost:4200

### Running tests

If you're not using an IDE with a built-in test explorer, you can run the tests from the terminal:

```bash
dotnet test CompaniesRegistry.sln
```

