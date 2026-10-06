# MetricNest

MetricNest is a .NET service monitoring project designed to track the health of HTTP services and provide a foundation for uptime monitoring, incident detection, and observability.

The project is being built incrementally with a clean separation between domain logic, application logic, and the API layer.

## Tech Stack

- .NET 10
- ASP.NET Core Web API
- C#
- HttpClient
- xUnit
- Clean layered architecture

## Project Structure

```text
MetricNest
├── src
│   ├── MetricNest.Api
│   ├── MetricNest.Application
│   └── MetricNest.Domain
└── tests
    └── MetricNest.Application.Tests
```

### MetricNest.Domain

Contains the core domain model and business concepts.

Current domain objects include:

- `MonitoredService`
- `ServiceStatus`

A monitored service stores information such as:

- Name
- URL
- Current health status
- Creation time
- Last health-check time

### MetricNest.Application

Contains application and monitoring logic.

Current functionality includes:

- HTTP status code evaluation
- HTTP service health checks
- `IServiceHealthChecker` abstraction
- `ServiceHealthChecker`
- `ServiceHealthEvaluator`

Service health is currently classified as:

| HTTP Result                  | Status   |
| ---------------------------- | -------- |
| 2xx                          | Healthy  |
| 5xx                          | Down     |
| Other responses              | Degraded |
| Connection failure / timeout | Down     |

### MetricNest.Api

The ASP.NET Core API layer.

This layer will expose MetricNest monitoring functionality through HTTP endpoints as the project develops.

## Current Flow

```text
Monitored Service
       |
       v
ServiceHealthChecker
       |
       v
HTTP Request
       |
       v
ServiceHealthEvaluator
       |
       v
Healthy / Degraded / Down
```

## Build

Clone the repository and run:

```bash
dotnet restore
dotnet build
```

## Tests

Run the automated tests with:

```bash
dotnet test
```

## Roadmap

Planned development includes:

- Service registration API
- Periodic background health checks
- Health-check history
- Persistent storage with Entity Framework Core
- SQL database integration
- Incident detection
- Uptime statistics
- Monitoring dashboard endpoints
- Logging and structured error handling
- Docker support
- CI/CD pipeline
- Deployment

## Goal

MetricNest is being developed as a backend-focused project demonstrating practical .NET development concepts including:

- Domain modeling
- Dependency inversion
- Dependency injection
- Async HTTP communication
- Automated testing
- Background processing
- Persistence
- API design
- DevOps and CI/CD

## Author

Parham Aziznejad

GitHub: [parham78](https://github.com/parham78)
