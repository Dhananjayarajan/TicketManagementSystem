# 🎫 Helpdesk Ticket Management System

A full-featured helpdesk ticket management system built with **ASP.NET Core 9.0**, featuring both a web interface and RESTful API. This application demonstrates clean architecture principles, CQRS pattern with MediatR, and modern web development practices.

---

## 📋 Table of Contents

- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Getting Started](#-getting-started)
- [Project Structure](#-project-structure)
- [API Documentation](#-api-documentation)
- [Web Interface](#-web-interface)
- [Authentication](#-authentication)
- [Database](#-database)
- [Architecture](#-architecture)
- [Screenshots](#-screenshots)
- [Contributing](#-contributing)
- [License](#-license)

---

## ✨ Features

### Core Functionality

- ✅ **Complete CRUD Operations** for tickets
- ✅ **Comment System** - Add, view, and delete comments on tickets
- ✅ **Status Management** - Track tickets through Open → In Progress → Resolved
- ✅ **Priority Levels** - Low, Medium, High priority classification
- ✅ **Search & Filter** - Filter tickets by status and priority

### Technical Features

- ✅ **RESTful API** with standardized JSON responses
- ✅ **Swagger/OpenAPI** documentation
- ✅ **Cookie-based Authentication**
- ✅ **CQRS Pattern** with MediatR
- ✅ **Clean Architecture** with clear separation of concerns
- ✅ **Entity Framework Core** with SQLite
- ✅ **Responsive Bootstrap UI**
- ✅ **Input Validation** (client and server-side)

---

## 🛠 Technology Stack

### Backend

- **ASP.NET Core 9.0** - Web framework
- **Entity Framework Core 9.0** - ORM
- **SQLite** - Database
- **MediatR 11.1** - CQRS implementation
- **Swashbuckle 6.5** - Swagger/OpenAPI

### Frontend

- **Razor Pages** - Server-side rendering
- **Bootstrap 5** - UI framework
- **Bootstrap Icons** - Icon library
- **Vanilla JavaScript** - Client-side interactions

### Architecture Patterns

- **CQRS** (Command Query Responsibility Segregation)
- **Clean Architecture** (Domain, Application, Infrastructure layers)
- **Repository Pattern** (via EF Core DbContext)
- **Dependency Injection**

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- Any code editor (Visual Studio 2022, VS Code, or Rider)

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/yourusername/helpdesk-ticket-system.git
   cd helpdesk-ticket-system/TicketProject
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Build the project**

   ```bash
   dotnet build
   ```

4. **Run the application**

   ```bash
   dotnet run
   ```

5. **Access the application**
   - Web UI: `https://localhost:5254` or `http://localhost:5000`
   - Swagger API: `https://localhost:5254/swagger`

### Quick Start with Sample Data

1. Navigate to the home page
2. Click "Create New Ticket"
3. Fill in the form and submit
4. View your ticket in the tickets list
5. Click on a ticket to view details and add comments

---

## 📁 Project Structure

```
TicketProject/
├── Application/               # Business logic layer
│   ├── Comment/              # Comment commands & queries
│   │   ├── CreateCommentCommand
│   │   ├── GetCommentsByTicketQuery
│   │   └── DeleteCommentCommand
│   └── Ticket/               # Ticket commands & queries
│       ├── Create/           # CreateTicketCommand
│       ├── GetAll/           # GetAllTicketsQuery
│       ├── GetById/          # GetTicketByIdQuery
│       ├── Update/           # UpdateTicketCommand
│       └── Delete/           # DeleteTicketCommand
├── Controllers/              # MVC & API Controllers
│   ├── HomeController.cs
│   ├── TicketController.cs        # MVC Controller
│   ├── TicketAPIController.cs     # API Controller
│   ├── CommentAPIController.cs
│   └── AccountController.cs
├── Domain/                   # Domain entities
│   ├── TicketEntity.cs
│   └── Comment.cs
├── Models/                   # DTOs & Request/Response models
│   └── SharedModels.cs
├── Persistance/              # Data access layer
│   └── AppDbContext.cs
├── Views/                    # Razor views
│   ├── Home/
│   ├── Ticket/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Details.cshtml
│   │   └── Edit.cshtml
│   ├── Account/
│   │   └── Login.cshtml
│   └── Shared/
│       └── _Layout.cshtml
├── wwwroot/                  # Static files
├── Program.cs                # Application startup
└── appsettings.json         # Configuration
```

---

## 📡 API Documentation

### Base URL

```
https://localhost:5254/api
```

### Response Format

All API responses follow this standardized format:

```json
{
  "success": true,
  "message": "Operation completed successfully",
  "data": { ... }
}
```

---

### 🎫 Tickets Endpoints

#### Get All Tickets

```http
GET /api/tickets
```

**Query Parameters:**

- `status` (optional): Filter by status (`Open`, `In Progress`, `Resolved`)
- `priority` (optional): Filter by priority (`Low`, `Medium`, `High`)

**Example:**

```bash
curl -X GET "https://localhost:5254/api/tickets?status=Open&priority=High"
```

**Response:**

```json
{
  "success": true,
  "message": "Tickets retrieved successfully",
  "data": [
    {
      "id": 1,
      "title": "Login issue",
      "status": "Open",
      "priority": "High",
      "createdDate": "2025-12-18T10:30:00"
    }
  ]
}
```

---

#### Get Ticket by ID

```http
GET /api/tickets/{id}
```

**Example:**

```bash
curl -X GET "https://localhost:5254/api/tickets/1"
```

**Response:**

```json
{
  "success": true,
  "message": "Ticket retrieved successfully",
  "data": {
    "id": 1,
    "title": "Login issue",
    "description": "Users unable to login",
    "status": "Open",
    "priority": "High",
    "createdDate": "2025-12-18T10:30:00",
    "comments": [
      {
        "id": 1,
        "text": "Looking into this",
        "author": "John Doe",
        "createdDate": "2025-12-18T11:00:00"
      }
    ]
  }
}
```

---

#### Create Ticket

```http
POST /api/tickets
```

**Request Body:**

```json
{
  "title": "Login issue",
  "description": "Users are unable to login to the system",
  "priority": "High"
}
```

**Example:**

```bash
curl -X POST "https://localhost:5254/api/tickets" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Login issue",
    "description": "Users unable to login",
    "priority": "High"
  }'
```

**Response:**

```json
{
  "success": true,
  "message": "Ticket created successfully",
  "data": {
    "id": 1
  }
}
```

---

#### Update Ticket

```http
PUT /api/tickets/{id}
```

**Request Body:**

```json
{
  "title": "Updated title",
  "description": "Updated description",
  "status": "In Progress",
  "priority": "Medium"
}
```

**Example:**

```bash
curl -X PUT "https://localhost:5254/api/tickets/1" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Login issue - RESOLVED",
    "description": "Issue has been fixed",
    "status": "Resolved",
    "priority": "High"
  }'
```

---

#### Update Ticket Status Only

```http
PATCH /api/tickets/{id}/status
```

**Request Body:**

```json
{
  "status": "In Progress"
}
```

**Example:**

```bash
curl -X PATCH "https://localhost:5254/api/tickets/1/status" \
  -H "Content-Type: application/json" \
  -d '{"status": "In Progress"}'
```

---

#### Delete Ticket

```http
DELETE /api/tickets/{id}
```

**Example:**

```bash
curl -X DELETE "https://localhost:5254/api/tickets/1"
```

**Response:**

```json
{
  "success": true,
  "message": "Ticket deleted successfully",
  "data": {
    "id": 1
  }
}
```

---

### 💬 Comments Endpoints

#### Get Ticket Comments

```http
GET /api/tickets/{id}/comments
```

**Example:**

```bash
curl -X GET "https://localhost:5254/api/tickets/1/comments"
```

**Response:**

```json
{
  "success": true,
  "message": "Comments retrieved successfully",
  "data": [
    {
      "id": 1,
      "text": "Working on this issue",
      "author": "John Doe",
      "createdDate": "2025-12-18T11:00:00"
    }
  ]
}
```

---

#### Add Comment to Ticket

```http
POST /api/tickets/{id}/comments
```

**Request Body:**

```json
{
  "text": "This has been resolved",
  "author": "Jane Smith"
}
```

**Example:**

```bash
curl -X POST "https://localhost:5254/api/tickets/1/comments" \
  -H "Content-Type: application/json" \
  -d '{
    "text": "Fixed the database connection",
    "author": "Admin"
  }'
```

---

#### Delete Comment

```http
DELETE /api/comments/{id}
```

**Example:**

```bash
curl -X DELETE "https://localhost:5254/api/comments/1"
```

---

## 🖥 Web Interface

### Available Pages

#### Home Page

- **URL:** `/`
- **Description:** Landing page with quick access to all features
- **Features:**
  - View all tickets button
  - Create new ticket button
  - API documentation link

#### Tickets List

- **URL:** `/Ticket/Index` or `/Ticket`
- **Description:** Browse all tickets
- **Features:**
  - Sortable table view
  - Status and priority badges
  - Action buttons (View, Edit, Delete)
  - Create new ticket button

#### Ticket Details

- **URL:** `/Ticket/Details/{id}`
- **Description:** View detailed ticket information
- **Features:**
  - Full ticket information
  - Comments section
  - Add new comments
  - Delete comments
  - Edit ticket button

#### Create Ticket

- **URL:** `/Ticket/Create`
- **Description:** Create a new support ticket
- **Features:**
  - Form validation
  - Priority selection
  - Rich text input

#### Edit Ticket

- **URL:** `/Ticket/Edit/{id}`
- **Description:** Modify existing ticket
- **Features:**
  - Pre-filled form
  - Update all fields
  - Status change

#### Login

- **URL:** `/Account/Login`
- **Description:** User authentication
- **Features:**
  - Cookie-based authentication
  - Remember me option
  - Demo credentials provided

---

## 🔐 Authentication

### Demo Credentials

The system includes simple cookie-based authentication for demonstration purposes.

**Admin Account:**

- Username: `admin`
- Password: `admin123`

**User Account:**

- Username: `user`
- Password: `user123`

### Implementation Details

- **Type:** Cookie-based authentication
- **Session Duration:** 24 hours
- **Technology:** ASP.NET Core Identity (simplified)

### Usage

1. Navigate to `/Account/Login`
2. Enter credentials
3. Session remains active for 24 hours
4. Click logout to end session

> ⚠️ **Note:** This is a demo authentication system. In production, implement proper ASP.NET Core Identity with password hashing, role-based access control, and secure practices.

---

## 💾 Database

### Database Provider

- **Type:** SQLite
- **File:** `ticketing.db`
- **Location:** Project root directory

### Schema

#### Tickets Table

| Column      | Type     | Description                     |
| ----------- | -------- | ------------------------------- |
| Id          | INTEGER  | Primary key                     |
| Title       | TEXT     | Ticket title (required)         |
| Description | TEXT     | Detailed description (required) |
| Status      | TEXT     | Open/In Progress/Resolved       |
| Priority    | TEXT     | Low/Medium/High                 |
| CreatedDate | DATETIME | Creation timestamp              |

#### Comments Table

| Column      | Type     | Description            |
| ----------- | -------- | ---------------------- |
| Id          | INTEGER  | Primary key            |
| Text        | TEXT     | Comment content        |
| Author      | TEXT     | Comment author name    |
| TicketId    | INTEGER  | Foreign key to Tickets |
| CreatedDate | DATETIME | Creation timestamp     |

### Database Operations

**View database:**

```bash
sqlite3 ticketing.db
.tables
SELECT * FROM Tickets;
SELECT * FROM Comments;
.exit
```

**Reset database:**

```bash
rm ticketing.db
dotnet run  # Database auto-creates on startup
```

---

## 🏗 Architecture

### CQRS with MediatR

The application uses the **Command Query Responsibility Segregation (CQRS)** pattern:

**Commands** (Write operations):

- `CreateTicketCommand`
- `UpdateTicketCommand`
- `DeleteTicketCommand`
- `CreateCommentCommand`
- `DeleteCommentCommand`

**Queries** (Read operations):

- `GetAllTicketsQuery`
- `GetTicketByIdQuery`
- `GetCommentsByTicketQuery`

### Clean Architecture Layers

```
┌─────────────────────────────────────┐
│         Presentation Layer          │
│    (Controllers, Views, APIs)       │
├─────────────────────────────────────┤
│         Application Layer           │
│   (Commands, Queries, Handlers)     │
├─────────────────────────────────────┤
│           Domain Layer              │
│       (Entities, Models)            │
├─────────────────────────────────────┤
│       Infrastructure Layer          │
│  (DbContext, Data Access)           │
└─────────────────────────────────────┘
```

### Design Patterns Used

1. **CQRS** - Separate read and write operations
2. **Mediator Pattern** - Loose coupling via MediatR
3. **Repository Pattern** - Via EF Core DbContext
4. **Dependency Injection** - Built-in ASP.NET Core DI
5. **DTO Pattern** - Data transfer objects for API responses

---

## 📸 Screenshots

### Home Page

The landing page provides quick access to all features with a clean, modern interface.

### Tickets List

Browse all tickets with color-coded priority and status badges. Filter and search capabilities make finding tickets easy.

### Ticket Details

View complete ticket information with all associated comments. Add new comments and manage ticket status in one place.

### Swagger API Documentation

Interactive API documentation allows testing endpoints directly from the browser.

---

## 🧪 Testing the Application

### Manual Testing

1. **Create a Ticket**

   ```bash
   curl -X POST "https://localhost:5254/api/tickets" \
     -H "Content-Type: application/json" \
     -d '{"title":"Test Ticket","description":"This is a test","priority":"High"}'
   ```

2. **Get All Tickets**

   ```bash
   curl -X GET "https://localhost:5254/api/tickets"
   ```

3. **Add a Comment**

   ```bash
   curl -X POST "https://localhost:5254/api/tickets/1/comments" \
     -H "Content-Type: application/json" \
     -d '{"text":"Test comment","author":"Tester"}'
   ```

4. **Update Status**
   ```bash
   curl -X PATCH "https://localhost:5254/api/tickets/1/status" \
     -H "Content-Type: application/json" \
     -d '{"status":"In Progress"}'
   ```

### Using Swagger UI

1. Navigate to `https://localhost:5254/swagger`
2. Expand any endpoint
3. Click "Try it out"
4. Fill in parameters
5. Click "Execute"
6. View response

---

## 🔧 Configuration

### Database Connection

Located in `Program.cs`:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("DataSource=ticketing.db"));
```

To use a different database:

1. Install the appropriate EF Core provider
2. Update the connection string
3. Update the `UseSqlite()` call

### Swagger Configuration

Located in `Program.cs`:

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Helpdesk Ticket Management API",
        // ...
    });
});
```

---

## 📦 NuGet Packages

```xml
<PackageReference Include="MediatR" Version="11.1.0" />
<PackageReference Include="MediatR.Extensions.Microsoft.DependencyInjection" Version="11.1.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="9.0.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
```

---

## 🚀 Deployment

### Publishing the Application

```bash
# Publish for production
dotnet publish -c Release -o ./publish

# Run the published app
cd publish
dotnet TicketProject.dll
```

### Environment Configuration

**Development:**

```bash
export ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

**Production:**

```bash
export ASPNETCORE_ENVIRONMENT=Production
dotnet run
```

---

## 🐛 Troubleshooting

### Common Issues

**Database not found:**

```bash
# Delete and recreate
rm ticketing.db ticketing.db-shm ticketing.db-wal
dotnet run
```

**Port already in use:**

```bash
# Change port in Properties/launchSettings.json
# or use command line
dotnet run --urls="https://localhost:5001"
```

**Comments not showing:**

- Check database has comments: `sqlite3 ticketing.db "SELECT * FROM Comments;"`
- Verify foreign key: Comments.TicketId should match Tickets.Id
- Check browser console for JavaScript errors

---

## 🤝 Contributing

Contributions are welcome! Here's how you can help:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Development Guidelines

- Follow existing code style and conventions
- Add XML documentation comments for public APIs
- Test all changes thoroughly
- Update README if adding new features

---

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

## 👥 Authors

- **Your Name** - _Initial work_ - [YourGitHub](https://github.com/yourusername)

---

## 🙏 Acknowledgments

- ASP.NET Core team for excellent documentation
- MediatR library for CQRS implementation
- Bootstrap team for the UI framework
- Community contributors

---

## 📞 Support

For support, email support@helpdesk.com or open an issue in the GitHub repository.

---

## 🗺 Roadmap

Future enhancements planned:

- [ ] Email notifications for ticket updates
- [ ] File attachments for tickets
- [ ] Real-time updates with SignalR
- [ ] Advanced search and filtering
- [ ] Ticket assignment system
- [ ] SLA tracking
- [ ] Reporting and analytics dashboard
- [ ] Multi-language support
- [ ] Mobile app
- [ ] Integration with third-party services (Slack, Teams)

---

## 📊 Project Stats

- **Lines of Code:** ~3,000
- **Files:** 50+
- **API Endpoints:** 9
- **Web Pages:** 8
- **Database Tables:** 2

---

**Made with ❤️ using ASP.NET Core**
