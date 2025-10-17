# API Gateway Project

A .NET 8.0 microservices-based API Gateway project utilizing Ocelot to route requests to `Weather` and `User` microservices. It incorporates custom middleware for request interception and access restriction, demonstrating a secure and modular microservice architecture.

## Features

- **API Gateway**: Routes requests to downstream services using Ocelot.
- **Microservices**:
  - `Weather`: Provides random weather forecasts.
  - `User`: Returns sample user data.
- **Middleware**:
  - `InterceptionMiddleware`: Adds `Referrer: Api-Gateway` header to all requests.
  - `RestrictAccessMiddleware`: Blocks direct access to services without a valid `Referrer` header (HTTP 403).
- **Swagger**: API documentation for `Weather` and `User` services in development mode.

## Project Structure

```
ApiGateway/
├── ApiGateway.sln
├── ApiGateway/                 # Ocelot-based API Gateway
│   ├── ocelot.json            # Route definitions
│   ├── Middleware/
│   │   └── InterceptionMiddleware.cs
│   ├── appsettings.json
│   ├── Program.cs
│   └── ...
├── Weather/                    # Weather forecast microservice
│   ├── Controllers/
│   │   └── WeatherForecastController.cs
│   ├── WeatherForecast.cs
│   ├── appsettings.json
│   ├── Program.cs
│   └── ...
├── User/                       # User data microservice
│   ├── Controllers/
│   │   └── UserController.cs
│   ├── appsettings.json
│   ├── Program.cs
│   └── ...
└── SharedLibrary/              # Shared middleware
    ├── RestrictAccessMiddleware.cs
    └── SharedLibrary.csproj
```

## Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/ApiGateway.git
   cd ApiGateway
   ```

2. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

3. **Run the Services**:
   - Open `ApiGateway.sln` in Visual Studio and set `ApiGateway`, `Weather`, and `User` as startup projects, or run individually:
     ```bash
     cd ApiGateway && dotnet run
     cd ../Weather && dotnet run
     cd ../User && dotnet run
     ```

4. **Access Endpoints**:
   - **Via Gateway**:
     - `https://localhost:7000/api/user` → Returns `["Ashik", "Rony", "Maddy"]`
     - `https://localhost:7000/api/WeatherForecast` → Returns 5 random weather forecasts
     - Example:
       ```bash
       curl -k https://localhost:7000/api/user
       # ["Ashik","Rony","Maddy"]
       ```
   - **Direct Access** (Blocked by Middleware):
     - `http://localhost:7001/api/user` → Returns `Hmmm, Can't reach this page` (HTTP 403)
     - `http://localhost:7002/api/WeatherForecast` → Returns `Hmmm, Can't reach this page` (HTTP 403)
     - Example:
       ```bash
       curl http://localhost:7001/api/user
       # Hmmm, Can't reach this page
       ```

5. **Swagger Documentation**:
   - Weather: `http://localhost:7002/swagger`
   - User: `http://localhost:7001/swagger`

## Middleware Details

- **InterceptionMiddleware** (`ApiGateway/Middleware/InterceptionMiddleware.cs`):
  - Injects `"Referrer": "Api-Gateway"` header to all downstream requests.
  - Ensures downstream services recognize requests as coming from the gateway.

- **RestrictAccessMiddleware** (`SharedLibrary/RestrictAccessMiddleware.cs`):
  - Checks for the presence of a `Referrer` header.
  - Returns HTTP 403 (`Hmmm, Can't reach this page`) if the header is missing.
  - Enforces access to services only through the API Gateway.

## Configuration

- **Ocelot** (`ApiGateway/ocelot.json`):
  - Routes:
    - `/api/user` → `http://localhost:7001/api/user`
    - `/api/WeatherForecast` → `http://localhost:7002/api/WeatherForecast`
  - Supports `GET`, `POST`, `PUT`, `DELETE` methods.
  - Base URL: `https://localhost:7000`

- **CORS**: Configured in `ApiGateway` to allow all headers, methods, and origins for development purposes.

## Endpoints

- **Weather Service** (`GET /api/WeatherForecast`):
  - Returns 5 random weather forecasts with date, temperature, and summary.
  - Example Response:
    ```json
    [
      {
        "date": "2025-10-18",
        "temperatureC": 25,
        "temperatureF": 77,
        "summary": "Warm"
      },
      ...
    ]
    ```

- **User Service** (`GET /api/user`):
  - Returns a list of sample user names.
  - Example Response:
    ```json
    ["Ashik", "Rony", "Maddy"]
    ```

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or compatible IDE
- Basic knowledge of ASP.NET Core and microservices

## Contributing

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/your-feature`).
3. Commit changes (`git commit -m "Add your feature"`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Open a pull request.
