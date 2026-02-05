# PrescriptionModule Architecture Overview

## Project Overview
The PrescriptionModule is a .NET 8.0 modular prescription management system built using a **Plugin-Based Architecture** with **Clean Architecture** principles. The system is designed to manage medical prescriptions, doctors, patients, medications, and related healthcare data.

## Solution Structure

The solution (`SWCoreSystem.sln`) is organized into three main folders:

### 1. **CoreSystem** Folder
- **AuthenticationSystem**: Main entry point/host application
  - Contains `Program.cs` - Application startup and configuration
  - Handles authentication, authorization, and plugin loading
  - Serves as the API host

### 2. **Plugins** Folder
Contains all feature modules (plugins) that are dynamically loaded:
- **Prescription** - Core prescription management
- **Doctor** - Doctor management
- **Patients** - Patient management
- **DoctorPrescription** (Medication) - Medication/prescription items
- **Diseases** - Disease management
- **Diagonosis** - Diagnosis management
- **Advice** - Medical advice management
- **Investigation** - Medical investigations/tests
- **Examinations** - Physical examinations
- **Symptoms** - Symptom management
- **Pharmacies** - Pharmacy management
- **Appointment** - Appointment scheduling
- **FollowUp** - Follow-up appointments
- **CommonHistory** - Patient history
- **DoctorChamber** - Doctor chamber/clinic management
- **DoctorDegree** - Doctor qualifications
- **DoctorExpertise** - Doctor specializations
- **DoctorSchedule** - Doctor schedules
- **ExpertiseCategory** - Medical expertise categories
- **Degree** - Educational degrees
- **Languages** - Language support
- **MedicationBrand** - Medication brands
- **MedicationManufacture** - Medication manufacturers
- **EmailTemplate** - Email templates
- **Notification** - Notification system
- **PrescriptionAdvice** - Prescription-specific advice
- **PrescriptionDiagonosis** - Prescription diagnoses
- **PrescriptionFollowUp** - Prescription follow-ups
- **PrescriptionInvestigation** - Prescription investigations
- **PrescriptionItems** - Prescription items/medications
- **PrescriptionPatientHistory** - Prescription patient history
- **PrescriptionPdf** - PDF generation for prescriptions
- **ScannedPrescription** - Scanned prescription handling
- **Schedule** - General scheduling
- **User** - User management
- **Tenants** - Multi-tenancy support
- **PDTCreator** - Prescription template creator
- **AdviceTranslations** - Advice translations

### 3. **Shared** Folder
Contains shared/common libraries:
- **Entities** - Domain entities/models
- **DataAccess** - Data access layer (not visible in current structure, but referenced)
- **Utility** - Utility classes and helpers
- **SharedService** - Shared services (JWT, mapping, common services)
- **DIService** - Dependency injection service registration
- **PluginDIService** - Plugin dependency injection interface
- **ApiCallService** - API call service for external integrations

## Architecture Layers

Each plugin module follows a consistent **Clean Architecture** pattern with the following layers:

### 1. **Controllers Layer** (`Controllers/`)
- API controllers handling HTTP requests
- Route: `api/2025-02/`
- Uses authorization policies for access control
- Returns standardized `ApiResponse<T>` responses

### 2. **Application Layer** (`Application/Services/`)
- Business logic and orchestration
- Service classes that coordinate between repositories
- Example: `PrescriptionService`, `DoctorService`

### 3. **Domain Layer** (`Domain/Repositories/`)
- Repository interfaces (contracts)
- Separated into:
  - **Query Repositories** (`I*QueryRepository`) - Read operations
  - **Command Repositories** (`I*CommandRepository`) - Write operations
- Follows CQRS pattern

### 4. **Infrastructure Layer** (`Insfracture/RepositoriesImplement/`)
- Repository implementations
- Data access implementations
- Uses `SqlDataAccessLayer` for database operations

### 5. **DTOs Layer** (`Dtos/`)
- **RequestDto** - Input models for API endpoints
- **ResponseDto** - Output models for API responses
- Organized by feature/entity

### 6. **Utility Layer** (`Utility/`)
- API response messages
- Constants
- Helper classes

## Key Architectural Patterns

### 1. **Plugin Architecture**
- Each module implements `IPlugin` interface
- `RegisterService.cs` in each module registers its dependencies
- Dynamic loading via `PluginLoader` class:
  ```csharp
  PluginLoader.LoadPlugins(services, pluginPath);  // Registers services
  PluginLoader.LoadPlugin(services, pluginPath);   // Registers controllers
  ```

### 2. **Dependency Injection**
- Centralized DI registration in `DIService`
- Each plugin registers its own services via `RegisterService`
- Scoped lifetime for most services

### 3. **Repository Pattern**
- Separation of concerns between query and command operations
- Interface-based design for testability
- Uses Dapper for data access

### 4. **CQRS (Command Query Responsibility Segregation)**
- Separate interfaces for queries and commands
- `I*QueryRepository` for read operations
- `I*CommandRepository` for write operations

### 5. **Multi-Tenancy**
- Tenant support via `Tenants` module
- TenantId used throughout the system

## Data Access

### Database
- **Database**: SQL Server (`SoowGood_System`)
- **ORM**: Dapper (micro-ORM)
- **Connection**: Configured in `appsettings.json`
- **Pattern**: Stored procedures preferred

### Data Access Layer
- `SqlDataAccessLayer` provides:
  - `LoadDataUsingProcedure<T, U>()` - Load list with parameters
  - `LoadSingleDataUsingProcedure<T, U>()` - Load single entity
  - `SaveDataUsingProcedure<T>()` - Save/update operations
  - `SaveDataUsingProcedureWithReturnId<T>()` - Save and return ID
  - Raw SQL execution methods

## Authentication & Authorization

### Authentication
- JWT Bearer token authentication
- Token validation configured in `Program.cs`
- Custom `TokenAuthenticationMiddleware` for token validation

### Authorization
- Policy-based authorization
- Custom `PermissionHandler` for permission checks
- Permission constants defined in `Utility.Permission`
- Policies registered via `AddCustomAuthorizationPolicies()`

## Key Features

### 1. **Prescription Management**
- Create, read, update, delete prescriptions
- Support for:
  - Medications (prescription items)
  - Diagnoses
  - Investigations/tests
  - Examinations
  - Advice
  - Patient history
  - Symptoms/chief complaints
  - Follow-up dates
- PDF generation using wkhtmltopdf
- Template support for reusable prescriptions

### 2. **PDF Generation**
- HTML to PDF conversion
- Custom prescription templates
- QR code generation (commented out)
- File serving via static file middleware

### 3. **AI Integration**
- Gemini AI integration for:
  - Prescription generation assistance
  - Chat-based interactions
  - AI-powered recommendations

### 4. **External API Integration**
- `ApiCallService` for calling external APIs
- Integration with SoowGood main system
- Notification service for SMS/email

### 5. **Multi-language Support**
- Translation modules for various entities
- Language management

## Configuration

### appsettings.json
- **ConnectionStrings**: Database connection
- **GeneralSettings**: Base URLs and API tokens
- **AppSettings**: PDF software path, file paths
- **PluginPaths**: Plugin directory configuration
- **Gemini**: AI API key

## Module Communication

Modules communicate through:
1. **Shared Services**: `SharedService`, `Utility`, `Entities`
2. **API Calls**: Via `ApiCallService` for cross-module communication
3. **Database**: Shared database with tenant isolation
4. **Dependency Injection**: Services registered and injected

## Technology Stack

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Dapper** (Micro-ORM)
- **SQL Server**
- **JWT Authentication**
- **Swagger/OpenAPI**
- **wkhtmltopdf** (PDF generation)
- **HtmlAgilityPack** (HTML parsing)
- **QRCoder** (QR code generation)
- **RestSharp** (HTTP client)

## Project Structure Example (Prescription Module)

```
Prescription/
├── Application/
│   └── Services/
│       ├── PrescriptionService.cs
│       ├── PrescriptionAdviceService.cs
│       └── ...
├── Controllers/
│   ├── PrescriptionController.cs
│   └── ...
├── Domain/
│   └── Repositories/
│       └── Prescription/
│           ├── IPrescriptionQueryRepository.cs
│           └── IPrescriptionCommandRepository.cs
├── Dtos/
│   ├── RequestDto/
│   └── ResponseDto/
├── Insfracture/
│   └── RepositoriesImplement/
│       └── Prescription/
│           ├── PrescriptionQueryRepository.cs
│           └── PrescriptionCommandRepository.cs
├── Utility/
│   └── PrescriptionApiConstantsResponseMessage.cs
└── RegisterService.cs
```

## Startup Flow

1. **Program.cs** initializes:
   - Configuration loading
   - Service registration (`AddDIServices()`)
   - Controller registration
   - Swagger configuration
   - CORS setup
   - Authentication/Authorization setup
   - Plugin loading (services and controllers)
   - Middleware pipeline setup

2. **Plugin Loading**:
   - Scans plugin directory for DLLs
   - Loads assemblies
   - Finds `IPlugin` implementations
   - Calls `RegisterServices()` on each plugin
   - Registers controllers via ApplicationPartManager

3. **Request Flow**:
   - Request → Middleware (CORS, Token Auth)
   - → Authentication/Authorization
   - → Controller
   - → Service
   - → Repository
   - → Database
   - → Response

## Best Practices Observed

1. **Separation of Concerns**: Clear layer separation
2. **Interface-Based Design**: Repository interfaces for abstraction
3. **Dependency Injection**: Proper DI usage throughout
4. **Error Handling**: Try-catch blocks with proper error responses
5. **Response Standardization**: Consistent `ApiResponse<T>` pattern
6. **Authorization**: Policy-based authorization on endpoints
7. **Modularity**: Each feature is a separate plugin
8. **Extensibility**: Easy to add new plugins without modifying core

## Areas for Potential Improvement

1. **Error Handling**: Could use global exception handling middleware
2. **Logging**: Structured logging (Serilog, NLog) not visible
3. **Validation**: Model validation could be more centralized
4. **Testing**: Test projects not visible in structure
5. **Documentation**: API documentation via Swagger is present
6. **Caching**: No visible caching strategy
7. **Transaction Management**: Uses `TransactionScope` in some places

## Database Schema

- Schema file located at: `Schema/Schema Design.sql`
- Entities defined in `Entities/EntityClass/`
- Supports multi-tenancy via `TenantId` fields

---

**Last Updated**: Based on codebase analysis
**Architecture Type**: Plugin-Based Clean Architecture
**Primary Pattern**: Repository Pattern with CQRS

