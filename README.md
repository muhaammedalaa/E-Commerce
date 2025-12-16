# Talabat E-Commerce Solution

This project is a multi-layered e-commerce solution designed to provide a robust and scalable platform for online retail operations. It leverages modern technologies and architectural patterns to ensure maintainability, extensibility, and high performance.

## Project Objectives

*   Develop a comprehensive API for all e-commerce functionalities.
*   Manage product catalogs, customer baskets, and order processing efficiently.
*   Implement secure user authentication and authorization.
*   Integrate various payment services.
*   Ensure a modular and scalable architecture for future growth and enhancements.

## Features

*   **Product Management**: Browse, view, and manage product listings.
*   **Shopping Basket**: Add, remove, and update items in a customer's shopping basket.
*   **Order Processing**: Create, track, and manage customer orders.
*   **User Authentication**: Secure user registration, login, and profile management.
*   **Payment Integration**: Support for various payment gateways (e.g., Stripe, PayPal - *webhook services like Stripe are mentioned in the docker-compose.yml*).
*   **Caching**: Improved performance through caching mechanisms.
*   **Error Handling**: Centralized error handling and API response standardization.

## Typical Order Flow

1. Create / Update Basket (Redis)
2. Select Delivery Method
3. Create Payment Intent (Stripe)
4. Complete Payment on Client
5. Create Order (SQL Server)
6. (Planned) Handle Stripe Webhook to update Order Payment Status

## Architecture

The project follows a clean, layered architecture, separating concerns into distinct projects:

*   **Talabat.APIs**: The presentation layer, exposing RESTful APIs for client applications. It handles HTTP requests, authentication, and delegates business logic to the service layer.
*   **Talabat.Core**: The domain layer, containing core entities, interfaces, DTOs (Data Transfer Objects), and specifications. This project defines the business rules and models.
*   **Talabat.Service**: The application service layer, encapsulating the business logic and orchestrating operations between the API and repository layers.
*   **Talabat.Repository**: The data access layer, responsible for interacting with the database. It implements generic repositories and specific data access logic.

## Design Patterns & Practices

- Repository Pattern
- Unit of Work
- Specification Pattern
- DTO Pattern
- Clean Architecture
- Dependency Injection

## Technologies Used

*   **Backend**: ASP.NET Core MVC (C#)
*   **Database**: SQL Server
*   **Caching**: Redis
*   **Containerization**: Docker, Docker Compose
*   **ORM**: Entity Framework Core
*   **Authentication**: ASP.NET Core Identity




## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Before you begin, ensure you have the following installed:

*   [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Docker Desktop](https://www.docker.com/products/docker-desktop/) (includes Docker Engine and Docker Compose)

### Building and Running the Application with Docker Compose

1.  **Clone the repository (if you haven't already):**

    ```bash
    git clone <repository_url>
    cd TalabatECommerce.solution
    ```

2.  **Build and run the Docker containers:**

    Navigate to the root directory of the project (where `docker-compose.yml` is located) and run:

    ```bash
    docker-compose up --build -d
    ```

    *   `--build`: This flag tells Docker Compose to build the images before starting containers. This is important when you make changes to the Dockerfile or project code.
    *   `-d`: This flag runs the containers in detached mode (in the background).

3.  **Verify services are running:**

    You can check the status of your running containers with:

    ```bash
    docker-compose ps
    ```

    You should see `talabat.apis`, `sqlserver`, and `redis` services listed as `Up`.

### Accessing Services

*   **Talabat.APIs (Backend)**:
    *   HTTP: `http://localhost:8080`
    *   HTTPS: `https://localhost:8081`

*   **SQL Server (Database)**:
    *   Accessible on `localhost:1433` from your host machine.
    *   Inside the Docker network, the hostname is `sqlserver`.

*   **Redis (Cache)**:
    *   Accessible on `localhost:6379` from your host machine.
    *   Inside the Docker network, the hostname is `redis`.

### Database Migrations

To apply database migrations, you will need to execute `dotnet ef database update` command within the `talabat.apis` container. 

1.  **Find the container ID or name for `talabat.apis`:**

    ```bash
    docker ps
    ```

2.  **Execute the migration command:**

    ```bash
    docker exec -it <talabat.apis_container_id_or_name> dotnet ef database update --project Talabat.Repository --startup-project Talabat.APIs
    docker exec -it <talabat.apis_container_id_or_name> dotnet ef database update --project Talabat.Repository.Identity --startup-project Talabat.APIs
    ```

    *Note: Replace `<talabat.apis_container_id_or_name>` with the actual ID or name of your running `talabat.apis` container.*

### Stopping and Removing Containers

To stop the running services and remove their containers, networks, and volumes (if not explicitly defined as external):

```bash
docker-compose down
```

To stop and remove containers but keep volumes (useful for preserving database data):

```bash
docker-compose down --remove-orphans
```

## Project Structure

*   `Talabat.APIs`: Contains the ASP.NET Core Web API project, controllers, and API-specific configurations.
*   `Talabat.Core`: Defines core interfaces, entities (e.g., `BaseEntity.cs`, `Prouduct.cs`), DTOs, and specifications for business rules.
*   `Talabat.Repository`: Implements the data access layer, including Entity Framework Core contexts (`Data/Contexts`), migrations, and generic/specific repositories (`Repositories`).
*   `Talabat.Service`: Houses the business logic and services (`Services`) that orchestrate operations and interact with the repository layer.
*   `TalabatECommerce.solution.AppHost`: Likely an application host project, possibly for orchestrating multiple services or for local development setup.
*   `TalabatECommerce.solution.ServiceDefaults`: Contains default service configurations and extensions.
*   `docker-compose.yml`: Defines the multi-container Docker application, including the backend API, SQL Server, and Redis.
*   `Talabat.APIs/Dockerfile`: Dockerfile for building the `Talabat.APIs` service image.[](url)
