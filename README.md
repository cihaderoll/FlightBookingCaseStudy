# Flight Booking Case Study

This repository contains a flight booking case study built with .NET 8. The project demonstrates a backend system for searching and booking flights, adhering to Clean Architecture principles and utilizing the CQRS pattern. It includes a Web API, a simple front-end client for interaction, and unit tests.

## Architectural Overview

The solution is structured following Clean Architecture principles, promoting separation of concerns, testability, and maintainability.

- **`Domain`**: Contains the core business entities such as `Order` and `Airport`. This layer has no dependencies on other layers.
- **`Application`**: Implements the business logic. It uses the CQRS pattern with MediatR to separate commands (actions that change state, like booking a flight) from queries (actions that read data, like searching for flights). It also defines interfaces for infrastructure-level concerns like data access and external services.
- **`Infrastructure`**: Provides concrete implementations for the interfaces defined in the Application layer. This includes:
    - **Persistence**: Using Entity Framework Core with a PostgreSQL database.
    - **Caching**: Using Redis to cache flight search results and reduce latency on subsequent identical searches.
    - **External Service Client**: An `HttpClient`-based client (`FlightProviderClient`) to communicate with an external SOAP-based flight provider service, including logic to parse the XML response.
- **`WebAPI`**: The entry point of the application. It exposes RESTful endpoints, handles requests, and forwards them to the Application layer via MediatR. It also includes global exception handling for a consistent error response format.

### Key Technologies & Patterns
- **.NET 8**
- **ASP.NET Core Web API**
- **Clean Architecture**
- **CQRS** with **MediatR**
- **Entity Framework Core** with **PostgreSQL**
- **Redis** for caching
- **FluentValidation** for request validation
- **AutoMapper** for object-to-object mapping
- **xUnit**, **Moq**, and **FluentAssertions** for unit testing

## Features

- **Flight Search**: Search for available flights based on origin, destination, and departure date.
- **Flight Booking**: Book a selected flight from the search results.
- **Result Caching**: Flight search results are cached in Redis to improve performance and avoid redundant calls to the external flight provider.
- **Airport Validation**: Origin and destination airport codes are validated against a list of valid airports in the database.
- **Simple UI**: A basic HTML/jQuery front-end is provided to demonstrate the API's functionality.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker (or a local installation of PostgreSQL and Redis)

### Setup

1.  **Clone the repository:**
    ```sh
    git clone https://github.com/cihaderoll/flightbookingcasestudy.git
    cd cihaderoll-flightbookingcasestudy
    ```

2.  **Configure Connection Strings:**
    Update the `ConnectionStrings` in `src/FlightBookingCaseStudy.WebAPI/appsettings.json` to point to your PostgreSQL and Redis instances.

    ```json
    "ConnectionStrings": {
      "Redis": "localhost:6379",
      "DefaultConnection": "Host=localhost;Port=5432;Database=FlightBookingDB;Username=your_username;Password=your_password;"
    }
    ```

3.  **Apply Database Migrations:**
    Ensure you have the EF Core tools installed (`dotnet tool install --global dotnet-ef`). Then, run the following command from the root directory to create and seed the database:

    ```sh
    dotnet ef database update --project src/FlightBookingCaseStudy.Infrastructure --startup-project src/FlightBookingCaseStudy.WebAPI
    ```
    *Note: The migrations will create the `Orders` and `Airports` tables. You may need to manually seed the `Airports` table with valid IATA codes for the search functionality to work.*

4.  **Run the Application:**
    Navigate to the WebAPI project and run the application.

    ```sh
    cd src/FlightBookingCaseStudy.WebAPI
    dotnet run
    ```
    The API will be available at `https://localhost:7242`.

## API Endpoints

The API is exposed through the `FlightController`.

### Search for Flights

- **Endpoint**: `GET /api/flights`
- **Description**: Retrieves a list of available flights based on the search criteria.
- **Query Parameters**:
    - `Origin` (string, required): The 3-letter IATA code for the departure airport (e.g., "IST").
    - `Destination` (string, required): The 3-letter IATA code for the arrival airport (e.g., "ADB").
    - `DepartDate` (date, required): The departure date in `YYYY-MM-DD` format.
    - `ReturnDate` (date, optional): The return date for round-trip searches.
- **Example Request**:
    `GET https://localhost:7242/api/flights?Origin=IST&Destination=ADB&DepartDate=2024-09-15`
- **Success Response (200 OK)**: An array of `FlightDto` objects.
    ```json
    [
        {
            "flightNumber": "TK2024",
            "departureDateTime": "2024-09-15T10:00:00Z",
            "arrivalDateTime": "2024-09-15T11:00:00Z",
            "price": 750.00,
            "flightId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
        }
    ]
    ```

### Book a Flight

- **Endpoint**: `POST /api/flights`
- **Description**: Creates a booking for a specific flight.
- **Request Body**:
    - `flightId` (string, required): The unique ID of the flight obtained from the search results.
- **Example Request Body**:
    ```json
    {
      "flightId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
    }
    ```
- **Success Response (200 OK)**: A GUID representing the unique booking/order ID.
    ```json
    "f0e9d8c7-b6a5-4f3e-2d1c-b0a9f8e7d6c5"
    ```

## Frontend Client

A simple user interface is located at `UI/index.html`. You can open this file directly in your web browser to interact with the running API. The client allows you to search for flights and book a flight from the results list. It handles API requests, displays results, and shows confirmation or error messages.

## Testing

The project includes a test suite in the `tests/FlightBookingCaseStudy.Test` project. These tests cover the application layer's command handlers using xUnit, Moq, and FluentAssertions to ensure the business logic works as expected.

To run the tests, execute the following command from the root directory:
```sh
dotnet test
