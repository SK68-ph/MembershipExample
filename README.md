# Membership API

## Overview

Membership API to manage user memberships, plans, and related data. Built using ASP.NET Core, this API attempts to follow the Clean Architecture principles, ensuring separation of concerns and maintainability.

## Features

- **User Management**: Create, update, retrieve, and delete users.
- **Membership Management**: Manage memberships associated with users, including the activation and deactivation of memberships.
- **Plan Management**: Create, update, retrieve, and delete subscription plans.
- **Clean Architecture**: The API attempts to follow the Clean Architecture principles, ensuring a clear separation of concerns across layers.

## Layers

1. **Domain Layer**:
   - Contains the core entities (`User`, `Membership`, `Plan`).
   - Defines repository interfaces for each entity.
   
2. **Application Layer**:
   - Implements business logic using the MediatR pattern.
   - Contains DTOs, Commands, Queries, Handlers, Validators, and Mapping profiles.
   
3. **Infrastructure Layer**:
   - Implements repository interfaces and handles database operations using Entity Framework Core.
   - Configures the DbContext.
   
4. **API Layer**:
   - Exposes endpoints for managing users, memberships, and plans.
   - Uses MediatR to handle incoming HTTP requests.

## Getting Started

### Prerequisites

- .NET 6 SDK or later
- MySQL Server (or another compatible database provider)
- Visual Studio or another C# IDE

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-repository/membership-api.git
   ```

2. **Navigate to the solution directory**:
   ```bash
   cd membership-api
   ```

3. **Restore NuGet packages**:
   ```bash
   dotnet restore
   ```

4. **Set up the database**:
   - Ensure your connection string in `appsettings.json` is correctly configured for your database.
   - Run the following nuget console command to apply migrations:
     ```bash
	 Update-Database -Project MembershipExample.Infrastructure -StartupProject MembershipExample.Api
     ```

5. **Run the API**:
   ```bash
	dotnet run --project Membership.Presentation
   ```

6. **Access the Swagger UI**:
   - The API documentation can be accessed through Swagger UI at `https://localhost:44353/swagger` when running locally.

### Adding Migration
   ```bash
	 Add-Migration MigrationName -Project MembershipExample.Infrastructure -StartupProject MembershipExample.Api
   ```


### Project Structure

- **Membership.Domain**: Contains the core business entities and repository interfaces.
- **Membership.Application**: Contains the application logic, including handlers, commands, queries, and mappings.
- **Membership.Infrastructure**: Implements the repositories and configures the database context.
- **Membership.API**: Exposes the API endpoints and integrates the application and infrastructure layers.

### Technologies Used

- **ASP.NET Core**: Web API framework
- **Entity Framework Core**: ORM for database operations
- **MediatR**: Library for implementing the CQRS pattern
- **AutoMapper**: Object-object mapper
- **FluentValidation**: Validation library for .NET
- **BCryptNet-Next**: Cryptographic library for password hashing
