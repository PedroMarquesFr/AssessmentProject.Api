## AssessmentProject.API - ASP.NET 6
This is the solution Web API for the Data Acquisition.

****

## Tech Stack  


## Code
C# with ASP.NET 6.   


## CQRS

The Command and Query Responsibility Segregation (CQRS) pattern separates read and update operations for a data store. Implementing CQRS in an application can maximize its performance, scalability and security. The flexibility created by migrating to CQRS allows a system to better evolve over time and prevents update commands from causing merge conflicts at the domain level.  
More detailed explanation on the link below:  

- [CQRS pattern by Microsoft](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)



## Clean Architecture

We are following an architectural style called **Clean Architecture**.  

Some key architectural principles followed by this style are:  

- Dependency Inversion Principle: always depend on abstractions, never on implementations.  

- Explicit Dependencies: one class / method should explicitly ask for its dependencies. That can be achieved through Dependency Injection.
- Single Responsibility: each layer of the architecture is divided be responsibilities:
    - **Domain**: This will be the source of truth. It will contain all entities, enums, exceptions, interfaces and logic specific to the domain. It is framework-agnostic.
    - **Application**: This layer contains all application logic. It is dependant on the Domain layer, but has no dependencies on any other layer. It defines interfaces that are implemented by outside layers. For instance, if the application needs to access a notification a notification service, a new interface would be added to the application and an implementation would be created within infrastructure.
    - **Infrastructure**: This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.
    - **API**: This layer contain controllers that will receive requests from external world and translate them as *Commands*, which are the input for Use Cases in the application layer and will occur in post processing, or *Queries*, which will only retrieve data based on some parameters.  

  
  
![Onion architecture](https://miro.medium.com/max/1200/1*EN-joV0Cr_gMn8aX06iHNQ.jpeg)  


## Mediator Design Pattern

The Mediator design pattern defines an object that encapsulates how a set of objects interact. 

Mediator promotes loose coupling by keeping objects from referring to each other explicitly, and it lets you vary their interaction independently.

To Implement the Mediator is used the MediatR library.

- [The MediatR library documentation](https://github.com/jbogard/MediatR)

![Mediator Design Pattern](https://i.stack.imgur.com/ALlTE.png)


## Libraries without up to date
Serilog version 2.12.0 wasn't up to date because RabbitMQ package isn't compatible with Serilog version 3.0.1.


## Testing  

Each module should be tested through unit tests and integration tests.  
The unit tests should verify if:  

- Business logic is properly working;  
- Events are being properly raised;  
- All other logic required for module to work as expected, such as mapping profiles between domain and query models.  

The integration tests should ideally be performed directly through the Service API, as it automatically crosses all boundaries between modules and infrastructure.  



## Operations
- Docker: build locally, run anywhere.

****



## Run in Docker with Docker Compose
1) The first step is: Generate a Developer Secutity Certificate to SSL Web App:
- Open a PowerShell prompt and run: dotnet dev-certs https --clean
- After this, with the Powershell openned fire the command: dotnet dev-certs https --trust -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p appuser@123
2) Open a PowerShell terminal, go to in project solution folder (the same folder where is AssessmentProject.Api) and fire the command: docker-compose up
3) Open a browser and access the url: https://localhost:5001/swagger/index.html
