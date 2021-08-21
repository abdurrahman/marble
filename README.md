# Marble E-Commerce 
A merchandising management system developed with C#/.NET 5.0

> Marble is a metamorphic rock composed of recrystallized carbonate minerals, most commonly calcite or dolomite. Marble is commonly used for sculpture and as a building material. ~Wiki

## Prerequisites

* [.NET SDK (5.0)](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* Postgresql
* Docker

## Installation

```shell
git clone https://github.com/abdurrahman/marble.git
cd marble
dotnet build
```

## Migrations

Add
```shell
dotnet ef migrations add InitialCreate -c MarbleDbContext --startup-project Marble.WebApi/Marble.WebApi.csproj --project Marble.Infrastructure.Data/Marble.Infrastructure.Data.csproj 
dotnet ef migrations add InitialCreate -c EventStoreContext --startup-project Marble.WebApi/Marble.WebApi.csproj --project Marble.Infrastructure.Data/Marble.Infrastructure.Data.csproj
dotnet ef migrations add InitialCreate -c AuthDbContext --startup-project Marble.WebApi/Marble.WebApi.csproj --project Marble.Infrastructure.Identity/Marble.Infrastructure.Identity.csproj  
```

Update

```shell
dotnet ef database update -c MarbleDbContext --startup-project Marble.WebApi/Marble.WebApi.csproj --project Marble.Infrastructure.Data/Marble.Infrastructure.Data.csproj 
dotnet ef database update -c EventStoreContext --startup-project Marble.WebApi/Marble.WebApi.csproj --project Marble.Infrastructure.Data/Marble.Infrastructure.Data.csproj
dotnet ef database update -c AuthDbContext --startup-project Marble.WebApi/Marble.WebApi.csproj --project Marble.Infrastructure.Identity/Marble.Infrastructure.Identity.csproj
```

## Built with

* C#, .NET 5.0
* Posgresql
* EntityFramework Core
* FluentValidation
* Mapster
* MediatR
* Swagger

## To Do

- [x] Data seeding
- [x] Health check
- [ ] Authorization & Authentication
- [ ] Unit Testing
- [ ] Docker
- [ ] Kafka/RabbitMQ
- [ ] Globalization

## License
[MIT](LICENSE.md)