# Operators Middleware

## Overview
Operators API is a middleware application built with C# .NET Core 8, implementing the Repository Pattern and utilizing Entity Framework Core 8 for database operations. This API serves as the backend middleware for the Operators Application.

## Technologies
- .NET Core 8
- Entity Framework Core 8
- MS SQL Server
- Swagger for API documentation
- Visual Studio (recommended IDE)

## Prerequisites
Before you begin, ensure you have the following installed:
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) (recommended) or Visual Studio Code
- [.NET Core 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads)

## Getting Started

### 1. Clone the Repository
```bash
git clone [your-repository-url]
cd [repository-name]
```

### 2. Database Configuration
1. Open `appsettings.json`
2. Update the connection string to match your SQL Server instance:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YourServerName;Database=OperatorsDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Database Migration
Open Package Manager Console in Visual Studio and run:
```bash
Update-Database
```

### 4. Running the Application
1. Open the solution in Visual Studio
2. Build the solution (Ctrl + Shift + B)
3. Press F5 to run the application
4. Swagger UI will automatically open in your default browser at `https://localhost:[port]/swagger`

## Project Structure
The project follows the Repository Pattern with the following structure:

```
├── Controllers/
├── Models/
├── Repositories/
│   ├── Interfaces/
│   └── Implementations/
├── Services/
├── Data/
└── Startup.cs
```

## Dependency Injection
Services are configured in the `Startup.cs` file using the built-in .NET Core dependency injection container. New services should be registered in the `ConfigureServices` method.

## API Documentation
API endpoints can be accessed and tested through Swagger UI at:
```
https://localhost:[port]/swagger
```

## Database Access
- The application uses Entity Framework Core 8 as the ORM
- Database context is configured in `Startup.cs`
- Connection string is stored in `appsettings.json`
- Repository pattern is implemented for data access abstraction

## Development Guidelines
1. Follow the existing repository pattern when adding new features
2. Use dependency injection for new services
3. Update database migrations when modifying the data model
4. Document new endpoints in Swagger using XML comments

## Common Issues and Solutions
1. **Database Connection Issues**
   - Verify connection string in appsettings.json
   - Ensure SQL Server is running
   - Check Windows Authentication settings

2. **Migration Errors**
   - Ensure all migrations are up to date
   - Run `Update-Database` in Package Manager Console

## Contributing
1. Create a new branch for your feature
2. Follow the existing code style and patterns
3. Update documentation as needed
4. Submit a pull request with a clear description of changes

## License
[Your License Here]

