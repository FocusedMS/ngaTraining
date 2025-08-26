# Car Rental API

A .NET Web API for managing a car rental system with JWT and GitHub OAuth authentication.

## Features

### Vehicle Management
- ✅ Add Vehicle
- ✅ Show Vehicle
- ✅ Show By Vehicle Type
- ✅ Show Vehicle by Type and Available

### Customer Management
- ✅ Add Customer
- ✅ Show Customer
- ✅ Search Customer

### Authentication
- ✅ JWT Authentication (Login/Register)
- ✅ GitHub OAuth Authentication

## Project Structure

```
CarRental/
├── CarRental.API/           # Web API project
│   ├── Controllers/         # API controllers
│   ├── Program.cs          # Application configuration
│   └── appsettings.json    # Configuration settings
├── CarRental.Core/         # Domain models and DTOs
│   ├── Models/             # Entity models
│   └── DTOs/               # Data Transfer Objects
└── CarRental.Infrastructure/ # Data access and services
    ├── Data/               # Entity Framework context
    └── Services/           # Business services
```

## Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB for development)
- Visual Studio 2022 or VS Code

## Setup Instructions

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd CarRental
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Update configuration**
   - Open `CarRental.API/appsettings.json`
   - Update the JWT secret key for production
   - Add your GitHub OAuth credentials (optional)

4. **Run the application**
   ```bash
   dotnet run --project CarRental.API
   ```

5. **Access the API**
   - Swagger UI: https://localhost:7001/swagger
   - API Base URL: https://localhost:7001/api

## API Endpoints

### Authentication

#### JWT Authentication
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login with email and password
- `POST /api/auth/refresh` - Refresh access token
- `POST /api/auth/logout` - Logout user

#### GitHub OAuth
- `GET /api/auth/github` - Initiate GitHub OAuth flow
- `GET /api/auth/github/callback` - GitHub OAuth callback

### Vehicle Management

- `GET /api/vehicles` - Get all vehicles
- `GET /api/vehicles/{id}` - Get vehicle by ID
- `GET /api/vehicles/type/{vehicleType}` - Get vehicles by type
- `GET /api/vehicles/type/{vehicleType}/available` - Get available vehicles by type
- `POST /api/vehicles` - Add new vehicle
- `PUT /api/vehicles/{id}` - Update vehicle
- `DELETE /api/vehicles/{id}` - Delete vehicle

### Customer Management

- `GET /api/customers` - Get all customers
- `GET /api/customers/{id}` - Get customer by ID
- `GET /api/customers/search?query={searchTerm}` - Search customers
- `POST /api/customers` - Add new customer
- `PUT /api/customers/{id}` - Update customer
- `DELETE /api/customers/{id}` - Delete customer

## Database

The application uses Entity Framework Core with SQL Server. The database will be automatically created when you first run the application.

### Database Schema

#### Vehicles Table
- Id (Primary Key)
- Make
- Model
- VehicleType
- LicensePlate
- Year
- DailyRate
- IsAvailable
- Description
- CreatedAt
- UpdatedAt

#### Customers Table
- Id (Primary Key)
- FirstName
- LastName
- Email
- PhoneNumber
- Address
- City
- State
- ZipCode
- Country
- DateOfBirth
- DriverLicenseNumber
- CreatedAt
- UpdatedAt

#### Users Table
- Id (Primary Key)
- Username
- Email
- PasswordHash
- GitHubId
- GitHubUsername
- GitHubEmail
- RefreshToken
- RefreshTokenExpiryTime
- CreatedAt
- UpdatedAt

## Authentication

### JWT Authentication
1. Register a new user using `POST /api/auth/register`
2. Login using `POST /api/auth/login`
3. Use the returned access token in the Authorization header: `Bearer {token}`

### GitHub OAuth
1. Configure GitHub OAuth app in GitHub settings
2. Update `appsettings.json` with your GitHub credentials
3. Use `GET /api/auth/github` to initiate OAuth flow

## Example Usage

### Register a new user
```bash
curl -X POST "https://localhost:7001/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "john_doe",
    "email": "john@example.com",
    "password": "password123",
    "confirmPassword": "password123"
  }'
```

### Add a vehicle
```bash
curl -X POST "https://localhost:7001/api/vehicles" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {your-jwt-token}" \
  -d '{
    "make": "Toyota",
    "model": "Camry",
    "vehicleType": "Sedan",
    "licensePlate": "ABC123",
    "year": 2022,
    "dailyRate": 50.00,
    "description": "Comfortable sedan for daily use"
  }'
```

### Search customers
```bash
curl -X GET "https://localhost:7001/api/customers/search?query=john" \
  -H "Authorization: Bearer {your-jwt-token}"
```

## Configuration

### JWT Settings
```json
{
  "Jwt": {
    "Secret": "your-super-secret-key-with-at-least-32-characters",
    "Issuer": "CarRental",
    "Audience": "CarRental",
    "ExpiryInHours": 1
  }
}
```

### GitHub OAuth Settings
```json
{
  "GitHub": {
    "ClientId": "your-github-client-id",
    "ClientSecret": "your-github-client-secret"
  }
}
```

## Development

### Adding new features
1. Create models in `CarRental.Core/Models/`
2. Create DTOs in `CarRental.Core/DTOs/`
3. Update `CarRentalDbContext` in `CarRental.Infrastructure/Data/`
4. Create controllers in `CarRental.API/Controllers/`
5. Update `Program.cs` if needed

### Running tests
```bash
dotnet test
```

### Database migrations
```bash
dotnet ef migrations add InitialCreate --project CarRental.Infrastructure --startup-project CarRental.API
dotnet ef database update --project CarRental.Infrastructure --startup-project CarRental.API
```

## Security Considerations

- Change the default JWT secret key in production
- Use HTTPS in production
- Implement proper password hashing (currently using SHA256, consider bcrypt)
- Add rate limiting for API endpoints
- Implement proper CORS policies for production
- Add input validation and sanitization
- Implement proper error handling and logging

## License

This project is licensed under the MIT License.
