# Client Register Implementation Guide

## Overview
This document describes the MudBlazor-based Client Details management system with grid, pagination, form validation, and modal dialogs.

## Components Created

### 1. **Pages/ClientDetails.razor**
Main page component for displaying client details.

**Features:**
- MudBlazor DataGrid with sortable columns
- Server-side pagination (10 items per page)
- Edit and Delete action buttons in the last column
- Create New Client button in the header
- Sample data generation for testing
- Responsive card-based layout

**URL:** `/client-details`

**Key Methods:**
- `LoadClients()` - Fetches clients from API (currently uses sample data)
- `OpenCreateDialog()` - Opens form for new client
- `OpenEditDialog(client)` - Opens form for editing existing client
- `OpenDeleteConfirmation(client)` - Shows delete confirmation dialog

### 2. **Components/ClientDetailsForm.razor**
Modal dialog component for creating, editing, and deleting clients.

**Features:**
- `EditForm` with `DataAnnotationsValidator` for validation
- MudBlazor text fields for Client Name and Location ID
- MudBlazor numeric field for Number of Users
- Automatic display of Date Registered (read-only)
- `ValidationSummary` for displaying all validation errors
- Generic error message display for each field
- Delete button (visible only in edit mode)
- Supports both Create and Update operations

**Validations:**
- Client Name: Required, 2-150 characters
- Location ID: Required
- Number of Users: Required, 1-1,000,000

### 3. **Components/ConfirmationDialog.razor**
Reusable confirmation dialog component.

**Features:**
- Customizable content text
- Customizable button text
- Customizable button color
- Used for delete confirmation

## Integration Points

### Updated Files

#### **Program.cs**
- Added `using MudBlazor.Services;`
- Added `builder.Services.AddMudServices();`

#### **_Imports.razor**
- Added `@using CapturingDetails.Web.Components;`
- Added `@using CapturingDetails.Web.Models;`
- Added `@using MudBlazor;`

#### **App.razor**
```razor
<MudThemeProvider/>
<MudDialogProvider/>
<MudSnackbarProvider/>
```

#### **MainLayout.razor**
- Wrapped content with `<MudLayout>` and `<MudMainContent>`
- Added `<MudContainer>` with MaxWidth.Large for responsive design

#### **wwwroot/index.html**
- Added MudBlazor CSS and fonts
- Added Material Design Icons CDN
- Added MudBlazor JavaScript

#### **Pages/Index.razor**
- Updated with landing page and link to Client Details

## Data Models

### **Models/ClientDetailsDto.cs**
Data Transfer Object for client details.

```csharp
public class ClientDetailsDto
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public DateTime DateRegistered { get; set; }
    public string LocationId { get; set; }
    public int NumberOfUsers { get; set; }
}
```

## API Integration Points

The following methods need to be connected to your backend API:

### In **ClientDetails.razor**:
1. `LoadClients()` - Replace comment with:
   ```csharp
   Clients = await Http.GetFromJsonAsync<List<ClientDetailsDto>>("api/clients");
   ```

2. `DeleteClient(int clientId)` - Replace comment with:
   ```csharp
   await Http.DeleteAsync($"api/clients/{clientId}");
   ```

### In **ClientDetailsForm.razor**:
1. **Create** - Replace comment in `HandleValidSubmit()`:
   ```csharp
   await Http.PostAsJsonAsync("api/clients", FormModel);
   ```

2. **Update** - Add POST/PUT logic:
   ```csharp
   await Http.PutAsJsonAsync($"api/clients/{FormModel.Id}", FormModel);
   ```

3. **Delete** - Replace comment in `DeleteClient()`:
   ```csharp
   await Http.DeleteAsync($"api/clients/{FormModel.Id}");
   ```

## Features Implemented

✅ **MudBlazor Integration** - Full MudBlazor UI library added
✅ **Server-side Pagination** - 10 items per page with paginator
✅ **Sortable Columns** - Click column headers to sort
✅ **Create New Client** - Button in grid header opens form
✅ **Edit Client** - Edit button in grid opens pre-populated form
✅ **Delete Client** - Delete button with confirmation popup
✅ **Form Validation** - DataAnnotationsValidator with error messages
✅ **Modal Dialogs** - Uses MudBlazor's DialogService
✅ **Responsive Design** - Mobile-friendly layout
✅ **Snackbar Notifications** - Success/Error messages
✅ **Single Form Component** - Reused for Create, Update, and Delete

## How to Use

1. **Navigate to Client Details**
   - Click "View Client Details" on the home page or navigate to `/client-details`

2. **Create New Client**
   - Click "New Client" button
   - Fill in the form fields
   - Click "Create" to save

3. **Edit Client**
   - Click the Edit icon (pencil) in the Actions column
   - Modify the fields
   - Click "Update" to save

4. **Delete Client**
   - Click the Delete icon (trash) or the Delete button in the edit form
   - Confirm the deletion in the popup dialog

5. **Navigate Pages**
   - Use the pagination controls at the bottom of the grid
   - Change items per page (10, 25, 50)

## Validation Rules (from DataAnnotations)

- **ClientName**: 
  - Required
  - Length: 2-150 characters
  
- **LocationId**: 
  - Required
  
- **NumberOfUsers**: 
  - Required
  - Range: 1-1,000,000
  
- **DateRegistered**: 
  - Auto-filled with `DateTime.UtcNow` at creation
  - Read-only in the form

## Next Steps

1. **Replace Sample Data** - Connect to your backend API
2. **Add Unique Client Name Validation** - Implement `UniqueClientName` attribute
3. **Add Location Dropdown** - Replace LocationId text field with dropdown
4. **Add Filtering** - Add search/filter functionality
5. **Add Audit Trail** - Log user actions for compliance
6. **Add Permissions** - Implement role-based access control

## Troubleshooting

### MudBlazor not loading
- Ensure `MudThemeProvider` is in App.razor
- Check that CSS and JS files are loaded in index.html
- Run `dotnet restore` to ensure NuGet packages are installed

### Form validation not working
- Ensure `DataAnnotationsValidator` is in the EditForm
- Check that properties have proper DataAnnotation attributes

### Dialog not appearing
- Ensure `MudDialogProvider` is in App.razor
- Verify `DialogService` is injected correctly

## References

- [MudBlazor Documentation](https://www.mudblazor.com/)
- [Blazor EditForm](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.editform)
- [Data Annotations Validation](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations)
