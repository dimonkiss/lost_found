# Lost & Found Web Platform

## Project Description

**Lost & Found Web Platform** is a web application for university "Lost & Found" platform that allows students and university staff to report lost and found items, as well as search for them.

## Technologies

- **.NET 8.0** - main development platform
- **ASP.NET Core Razor Pages** - web framework
- **Bootstrap 5** - CSS framework for styling
- **jQuery** - JavaScript library
- **Font Awesome** - icons

## Functionality

### Main Features:

1. **Lost Items Management:**
   - Adding new lost items
   - Editing existing records
   - Deleting records
   - Viewing list of lost items

2. **Found Items Management:**
   - Adding new found items
   - Editing existing records
   - Deleting records
   - Viewing list of found items

3. **Search:**
   - Search through lost items
   - Search through found items
   - Universal search across all records

4. **Home Page:**
   - Overview of recent lost items
   - Overview of recent found items
   - Quick access to main functions

## Project Structure

```
lost_found_web/
├── Models/                 # Data models
│   ├── LostItem.cs        # Lost item model
│   └── FoundItem.cs       # Found item model
├── Services/              # Business logic
│   ├── ILostFoundService.cs    # Service interface
│   └── LostFoundService.cs     # Service implementation
├── Pages/                 # Razor Pages
│   ├── LostItems/         # Lost items pages
│   ├── FoundItems/        # Found items pages
│   ├── Search/            # Search pages
│   └── Shared/            # Shared components
├── wwwroot/               # Static files
│   ├── css/               # CSS styles
│   ├── js/                # JavaScript files
│   └── lib/                # External libraries
└── Program.cs             # Application entry point
```

## Data Models

### LostItem (Lost Item)
- `Id` - unique identifier
- `ItemName` - item name (required, up to 100 characters)
- `Description` - item description (required, up to 500 characters)
- `LostLocation` - location where item was lost (required, up to 100 characters)
- `LostDate` - date when item was lost (required)
- `ContactInfo` - contact information (required, up to 200 characters)
- `ImageUrl` - image URL (optional)
- `CreatedAt` - record creation date
- `IsFound` - whether the item has been found

### FoundItem (Found Item)
- `Id` - unique identifier
- `ItemName` - item name (required, up to 100 characters)
- `Description` - item description (required, up to 500 characters)
- `FoundLocation` - location where item was found (required, up to 100 characters)
- `FoundDate` - date when item was found (required)
- `ContactInfo` - contact information (required, up to 200 characters)
- `ImageUrl` - image URL (optional)
- `CreatedAt` - record creation date
- `IsClaimed` - whether the item has been claimed by owner

## Services

### ILostFoundService
Interface that defines basic operations for working with lost and found items:

**For Lost Items:**
- `GetLostItemsAsync()` - get all lost items
- `GetLostItemByIdAsync(int id)` - get lost item by ID
- `AddLostItemAsync(LostItem item)` - add new lost item
- `UpdateLostItemAsync(LostItem item)` - update lost item
- `DeleteLostItemAsync(int id)` - delete lost item
- `SearchLostItemsAsync(string searchTerm)` - search lost items

**For Found Items:**
- `GetFoundItemsAsync()` - get all found items
- `GetFoundItemByIdAsync(int id)` - get found item by ID
- `AddFoundItemAsync(FoundItem item)` - add new found item
- `UpdateFoundItemAsync(FoundItem item)` - update found item
- `DeleteFoundItemAsync(int id)` - delete found item
- `SearchFoundItemsAsync(string searchTerm)` - search found items

### LostFoundService
Service implementation using in-memory data storage. Includes sample data for functionality demonstration.

## Pages

### Home Page (/)
- Overview of recent lost and found items
- Quick access to main functions
- System navigation

### Lost Items Management (/LostItems)
- **Index** - list of all lost items
- **Create** - add new lost item
- **Edit** - edit lost item
- **Delete** - delete lost item

### Found Items Management (/FoundItems)
- **Index** - list of all found items
- **Create** - add new found item
- **Edit** - edit found item
- **Delete** - delete found item

### Search (/Search)
- **Index** - universal search across all records

## Data Validation

All models include validation using Data Annotations:
- Required fields
- String length constraints
- Error messages in English

## Running the Project

1. Make sure .NET 8.0 SDK is installed
2. Clone the repository
3. Navigate to the project folder:
   ```bash
   cd lost_found_web
   ```
4. Restore dependencies:
   ```bash
   dotnet restore
   ```
5. Run the project:
   ```bash
   dotnet run
   ```
6. Open browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## Testing

The project includes a test assembly `lost_found_web.Tests`. To run tests:

```bash
dotnet test
```

Or use the scripts:
- Windows: `run_tests.bat`
- PowerShell: `run_tests.ps1`

## Architecture

The application is built following these principles:
- **Separation of Concerns** - separation of responsibilities
- **Dependency Injection** - dependency injection
- **Repository Pattern** - repository pattern (through services)
- **MVC Pattern** - model-view-controller pattern

## Future Improvements

- Database integration (Entity Framework Core)
- Authentication and authorization system
- Image upload functionality
- Email notifications
- API for mobile applications
- Advanced search with filters
- Statistics and analytics

## Deployment

This project supports deployment across multiple operating systems using Vagrant and can be packaged as a NuGet package for distribution through a private BaGet repository.

### Quick Deployment

```powershell
# Deploy to all operating systems
.\quick-deploy.ps1

# Deploy to specific OS
.\quick-deploy.ps1 -Target windows
.\quick-deploy.ps1 -Target linux
.\quick-deploy.ps1 -Target macos

# Build and deploy package
.\quick-deploy.ps1 -Build -Deploy
```

### Manual Deployment

1. **Build NuGet Package**:
   ```powershell
   .\scripts\build-and-package.ps1
   ```

2. **Deploy to BaGet**:
   ```powershell
   .\scripts\deploy-to-baget.ps1
   ```

3. **Deploy with Vagrant**:
   ```bash
   vagrant up windows    # Windows deployment
   vagrant up linux      # Linux deployment
   vagrant up macos      # macOS deployment
   ```

### Access Points

After deployment, the application will be available at:

- **Windows**: http://192.168.56.10:5000
- **Linux**: http://192.168.56.11:5001
- **macOS**: http://192.168.56.12:5003

For detailed deployment instructions, see [DEPLOYMENT.md](DEPLOYMENT.md).

## License

This project is developed for educational purposes of the university platform.
