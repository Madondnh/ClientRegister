# Client Service Implementation

## Overview
The Client Service provides a centralized way to handle all CRUD (Create, Read, Update, Delete) operations for client details. It communicates with a backend API and automatically reads the API URL from the application settings.

## Files Created

### 1. **Services/IClientService.cs**
Interface defining the contract for client operations.

**Methods:**
- `GetAllClientsAsync()` - Retrieves all clients from the API
- `GetClientByIdAsync(id)` - Retrieves a specific client by ID
- `CreateClientAsync(client)` - Creates a new client
- `UpdateClientAsync(id, client)` - Updates an existing client
- `DeleteClientAsync(id)` - Deletes a client by ID

### 2. **Services/ClientService.cs**
Implementation of `IClientService` that handles HTTP communication with the backend API.

**Features:**
- Reads API base URL from `appsettings.json`
- Validates input parameters
- Handles HTTP errors and JSON deserialization errors
- Provides detailed error messages
- Uses dependency injection for HttpClient and IConfiguration

### 3. **wwwroot/appsettings.json**
Configuration file containing the API settings.

```json
{
  "ApiSettings": {
    "BaseUrl": "https://api.example.com"
  }
}
```

**To use your own API:**
1. Open `wwwroot/appsettings.json`
2. Update the `BaseUrl` to your backend API URL (e.g., "https://your-api.com")

## Updated Files

### Program.cs
- Loads `appsettings.json` configuration
- Registers `IClientService` as a scoped service
- HttpClient is properly configured

### _Imports.razor
- Added `@using CapturingDetails.Web.Services;` for easy access to the service

### ClientDetailsForm.razor
- Injected `IClientService`
- Updated `HandleValidSubmit()` to call `CreateClientAsync()` or `UpdateClientAsync()`
- Updated `DeleteClient()` to call `DeleteClientAsync()`

### ClientDetails.razor
- Injected `IClientService`
- Updated `LoadClients()` to call `GetAllClientsAsync()`
- Updated `DeleteClient()` to call `DeleteClientAsync()`

## Usage Example

In any Blazor component, inject the service:

```csharp
@inject IClientService ClientService

@code {
    private List<ClientDetails> clients;

    protected override async Task OnInitializedAsync()
    {
        // Get all clients
        clients = await ClientService.GetAllClientsAsync();
    }

    private async Task CreateNewClient()
    {
        var newClient = new ClientDetails 
        { 
            ClientName = "Acme Corp",
            LocationId = "LOC-001",
            NumberOfUsers = 50,
            DateRegistered = DateTime.UtcNow
        };
        
        var created = await ClientService.CreateClientAsync(newClient);
    }

    private async Task UpdateClient(ClientDetails client)
    {
        var updated = await ClientService.UpdateClientAsync(client.Id, client);
    }

    private async Task RemoveClient(string clientId)
    {
        await ClientService.DeleteClientAsync(clientId);
    }
}
```

## API Endpoints Expected

The service expects the following REST API endpoints:

- `GET /api/clients` - Get all clients
- `GET /api/clients/{id}` - Get client by ID
- `POST /api/clients` - Create new client
- `PUT /api/clients/{id}` - Update client
- `DELETE /api/clients/{id}` - Delete client

## Error Handling

The service throws `Exception` with descriptive messages for:
- Failed HTTP requests
- JSON deserialization errors
- Invalid input parameters
- Missing configuration

Always wrap service calls in try-catch blocks or use error handling in your components (already done in the updated components).

## Configuration Best Practices

1. **Development:** Set `BaseUrl` to your local development API
2. **Production:** Update `BaseUrl` to your production API URL before deployment
3. **Environment-specific settings:** Consider using environment variables for different deployment targets
