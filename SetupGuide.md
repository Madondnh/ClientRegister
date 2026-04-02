# Client Register Solution Setup Guide

## Overview
This guide provides step-by-step instructions for building, running database migrations, and running the multiple presentation projects in the Client Register solution using Visual Studio.

## Prerequisites
- **.NET 8.0 SDK** or later (download from [microsoft.com](https://dotnet.microsoft.com/download))
- **Visual Studio 2022** (Community, Professional, or Enterprise edition)
- **Git** for cloning the repository
- **SQL Server** or **SQLite** (the project uses SQLite by default)

## 1. Open the Solution in Visual Studio
1. Navigate to solution root folder
2. launch solution file with Visual Studio 2022
2. Allow the project to load 

## 2. Build the Solution
1. In Visual Studio, ensure the solution is loaded
2. From the menu, select **Build > Build Solution** (or press Ctrl+Shift+B)
3. Verify that all projects build successfully without errors

Alternatively, you can build from the command line:
```
dotnet build ClientRegister.sln
```

## 3. Run Database Migrations
The solution uses Entity Framework Core with SQLite. The database is automatically created when the application starts, but if you need to run migrations manually:

1. Open the Package Manager Console in Visual Studio:
   - View > Other Windows > Package Manager Console
2. Set the Default Project to **Infrastructure**
3. Set the Startup Project to **ClientManagement.API** (or another API project)
4. Run: `Update-Database`

Alternatively, paste the following comand in PMC:
```
dotnet ef database update  --project ./Infrastructure/Infrastructure.csproj  --startup-project ./Presentation/ClientManagement.Api/ClientManagement.Api.csproj
```

## 4 Reset the database (Optional)
1. Navigate to the SQLLite db file ( CleanArchitecture.db ) Solution > Src > Presentation  > ClientManagement.API > CleanArchitecture.db
2. Delete the file
3. Run Migrations in  Package Manager Console
paste the following comand in PMC:
```
dotnet ef database update  --project ./Infrastructure/Infrastructure.csproj  --startup-project ./Presentation/ClientManagement.Api/ClientManagement.Api.csproj
```

**Note:** The application uses `EnsureCreated()` which will create the database schema automatically on first run, so manual migration may not be necessary.

## 5. Configure Multiple Startup Projects
Since the solution has multiple presentation projects, you need to configure Visual Studio to start them simultaneously:

1. In Solution Explorer, right-click on the **ClientRegister** solution (top node)
2. Select **Set Startup Projects...**
3. In the dialog, select **Multiple startup projects**
4. For each of the following projects, set **Action** to **Start**:
   - **ClientManagement.API**
   - **CapturingDetails.Web**
   - **DisplayClientDetails.Web**
5. Click **Apply** then **OK**

## 6. Run the Solution
1. Press **F5** or click the green **Start** button in Visual Studio
2. Visual Studio will build and start all configured projects
3. The applications will open in your default browser or show console output

### Expected Behavior:
- **ClientManagement.API**: API server will start (typically on http://localhost:5000 or similar)
- **CapturingDetails.Web**: Web application will start
- **DisplayClientDetails.Web**: Web application will start

## 7. Verify Everything is Running
1. Check the Visual Studio output window for any errors
2. Open your browser and navigate to the URLs shown in the output
3. Test the applications to ensure they are functioning correctly

## Troubleshooting

### Build Errors
- Ensure all NuGet packages are restored: Build > Rebuild Solution
- Check that .NET 8.0 SDK is installed: `dotnet --version`

### Database Issues
- If using SQLite, ensure the `CleanArchitecture.db` file has write permissions
- Check the connection string in `appsettings.json` files

### Port Conflicts
- If ports are in use, modify the launch settings in each project's `Properties/launchSettings.json`

### Multiple Projects Not Starting
- Verify all projects are set to "Start" in the startup projects configuration
- Check that each project has a valid startup configuration

## Project Structure
- **Domain**: Contains entities, DTOs, and domain logic
- **Application**: Contains services and application logic
- **Infrastructure**: Contains database context, repositories, and migrations
- **Presentation**:
  - **ClientManagement.API**: REST API for client management
  - **CapturingDetails.Web**: Web interface for capturing client details
  - **DisplayClientDetails.Web**: Web interface for displaying client details

## Additional Notes
- The solution uses Clean Architecture principles
- SQLite is used for development; modify connection strings for production
- Swagger UI is available for API testing at the API endpoints
- CORS is configured to allow communication between web applications

If you encounter any issues not covered here, please check the project documentation or contact the development team.
