﻿# ✨ Quick Start Guide
> The code might look a little bit robust for this simple CRUD application, but it's easy to add new functionality.
Also I wanted to try out some new stuff like Minimal API and Source Generators, because why not :D 
Although I decided to not use Minimal API in the final product because of its limitations, I didn't remove it from the code.

### Deploy

# Project Structure
### Domain
Center of the Clean Architecture design, does not depend on other projects. Includes the Domain model, such as: 
- Aggregates
- Entities
- Value objects
- Domain events and handlers
- Domain services

### Application
The Application layer is organized to follow the CQRS pattern which separates the flow into Commands and Queries. 
It can be easily made by the mediator pattern. Queries are read-only requests, while Commands modifies the database.
I like to use the repository pattern to insert an additional layer between the data access layer and business logic.
This way the business logic does not depend on any persistance technology and can be easily tested by mocking the repositories.

- DTO classes and validators
- Interfaces
- Command & Query handlers
- Mapping configurations

### Infrastructure
Application dependencies and external resources should be implemented in the Infrastructure project.
Interfaces are mostly defined in the Application layer, this project should implement those abstractions.
The Infrastructure project depends on e.g. ```Microsoft.EntityFrameworkCore.InMemory``` and ```Microsoft.EntityFrameworkCore.Sqlite```.

- Repositories
- Middlewares
- Service implementations
- Database schema configurations
- Minimal API endpoints

### API
The entry point of the application. Includes the configuration file which is ```appsettings.json``` by default.

- Controllers
- AppSettings

# Further Improvements
I believe this sample project already goes well beyond the requirements, in case of expanding the functionality,
I've written some bullet points that should be considered in a production-ready environment.

- Specification pattern
- UnitOfWork pattern to handle multiple repositories
- Page-based or Cursor-based pagination, although only one month of leave times are queried
- Authentication and authorization
- Caching
- Healthcheck
- HATEOAS to make FE devs' lives easier
- Integration and Functional tests

# Technologies & Libraries
- .NET 8
- EF Core
- Swagger
- FluentValidation
- Mapster
- MediatR
- Serilog
- Moq
