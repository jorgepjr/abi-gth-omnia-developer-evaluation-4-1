# Store - API

API for managing orders, order items, and applying discounts based on the quantity of identical items added.

---

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop)

- [Visual Studio](https://visualstudio.microsoft.com/)

---

## Database with Docker Compose

The application uses PostgreSQL, which must be started manually via Docker Compose before running the API.

### Starting the database

Run the following command in the terminal, within the folder where the `docker-compose.yml` file is located:

```

docker-compose up -d

```

## Migrations

The migration files are already created and are located at the path:

```

[Migrations](../template/backend/src/Ambev.DeveloperEvaluation.WebApi/Migrations)

```

## Running the Project in Visual Studio

1 - Open the solution in Visual Studio.

2 - Select the main Web API project as the startup project.

3 - Press F5 or click Run.

4 - The API will start (using HTTP or HTTPS) and attempt to connect to the PostgreSQL database previously started via Docker.

5- After the application connects to the database, the tables will be automatically generated.

## Usage Instructions

To register an order, you must first register the following entities:

Customer

Shop

Product

After these registrations, use the order endpoints to create and manage orders

You can test the endpoints via Swagger:

http://localhost:5119/swagger/index.html

