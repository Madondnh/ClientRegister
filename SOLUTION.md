# Solution Design and Trade-offs

## Architecture Overview
The solution follows a layered architecture:

- **Presentation**
  - Catalog.API: ASP.NET Core Web API (Backend)
  - Catalog.Web: Angular Application (Frontend)
- **Application**
  - Application logic and use cases
- **Domain**
  - Core business entities and rules
- **Infrastructure**
  - Implementation of data persistence and external concerns
- **Tests**
  - Unit tests validating application behavior

This structure enforces strong separation of concerns and improves maintainability.

## Key Design Decisions

### Layered Architecture
Chosen to:
- Keep business logic independent of frameworks
- Improve testability and readability
- Allow future replacement of infrastructure concerns with minimal impact

### ASP.NET Core Web API
- Uses the modern minimal hosting model
- Leverages built-in dependency injection for loose coupling

### Configuration Strategy
- All configuration is externalized using `appsettings.json`
- Environment-specific overrides supported via `appsettings.Development.json`

### Seed Data
- Seed data is included to simplify local development and testing
- Avoids requiring external dependencies for initial setup

## Trade-offs

### Advantages
- Clear project boundaries
- Easy to extend and refactor
- Well-aligned with common enterprise .NET practices

### Disadvantages
- More structure and files compared to a single-project solution
- Slightly higher onboarding cost for developers unfamiliar with layered design

## Startup Project Configuration
Visual Studio startup project selection is stored in user-specific files and is intentionally not committed to Git.
Each developer must configure startup projects locally or use CLI commands.

## Testing Approach
- Unit tests focus on application-level logic
- Tests are executable via `dotnet test`
- No external services are required for test execution

## Future Improvements
- Introduce persistence using EF Core with a relational database
- Add integration and end-to-end tests
- Add Docker / Docker Compose for consistent local environments
- Implement authentication, authorization, and API versioning

## Assumptions
- Single API consumer type
- Local development environment without external infrastructure dependencies
