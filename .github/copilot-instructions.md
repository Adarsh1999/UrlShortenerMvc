# URL Shortener MVC - AI Agent Instructions

## Project Overview
ASP.NET Core MVC (.NET 9.0) URL shortener with PostgreSQL, tracking click analytics. Designed for simplicity and beginner-friendliness with clean, commented code.

## Architecture & Data Flow

### Core Components
- **Models**: `ShortUrl` (1-to-many) → `Click` with cascade delete configured in `AppDbContext`
- **Controllers**: `ShortUrlsController` handles creation, redirect tracking, and stats; `HomeController` for static pages
- **ViewComponents**: `ShortUrlSidebarViewComponent` displays all URLs in sidebar across pages
- **Data Layer**: Single `AppDbContext` with EF Core + PostgreSQL, unique index on `ShortUrl.ShortCode`

### URL Shortening Flow
1. User submits URL via `CreateUrlViewModel` → validated by `CreateUrlValidator` (FluentValidation)
2. `GenerateUniqueCodeAsync()` creates 4-char Base64-URL-safe code (RNG-based, collision-checked)
3. Default expiry: 15 days from creation if not specified
4. Short links redirect via custom route: `/{code}` → `ShortUrlsController.Go()`
5. Each redirect increments `ClickCount` and creates `Click` record with IP (X-Forwarded-For aware), referrer, user agent

### DateTime Handling Convention
**CRITICAL**: All `DateTime` properties use UTC internally. Always call `.ToUniversalTime()` when saving user input (see `Create` action). Display conversions handled in views with `.ToString("g")`.

## Development Workflow

### Database Setup & Migrations
```bash
# Start PostgreSQL + Adminer (http://localhost:8080)
docker-compose up -d

# Apply migrations (connection string in appsettings.Development.json)
dotnet ef database update

# Create new migration after model changes
dotnet ef migrations add MigrationName
```

### Running the Application
```bash
# Default runs on http://localhost:5265
dotnet run

# Build for production
dotnet build -c Release
```

## Key Conventions

### Validation Pattern
- Use FluentValidation for ViewModels (auto-registered via `AddValidatorsFromAssemblyContaining`)
- Client-side adapters enabled in `Program.cs` for unobtrusive validation
- Check `ModelState.IsValid` in controller POST actions

### Logging Configuration
EF Core SQL queries suppressed to Warning level in `Program.cs` to reduce noise during development.

### Routing Rules
Two route patterns configured:
1. `{code}` → `ShortUrlsController.Go` (URL redirect, **must be before default route**)
2. `{controller}/{action}/{id?}` → standard MVC pattern

### View Structure
- Shared `_Layout.cshtml` includes sidebar via `@await Component.InvokeAsync("ShortUrlSidebar")`
- Bootstrap 5 + FontAwesome for UI consistency
- Copy-to-clipboard functionality in `wwwroot/js/site.js`

## Common Patterns

### Adding New Features
1. **New entity**: Create in `Models/`, add `DbSet<T>` to `AppDbContext`, configure relationships in `OnModelCreating`
2. **Validation**: Create validator in `Validators/` extending `AbstractValidator<T>`
3. **View-specific data**: Use ViewModels in `ViewModels/` instead of exposing entities directly
4. **Analytics tracking**: Follow `Click` model pattern (IP extraction from X-Forwarded-For, handle comma-separated lists)

### IP Address Extraction Pattern
```csharp
string ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
if (string.IsNullOrWhiteSpace(ip))
    ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "n/a";
else if (ip.Contains(","))
    ip = ip.Split(',')[0].Trim(); // Proxy chain: take first IP
```

## External Dependencies
- **PostgreSQL** (Docker): Default credentials in `docker-compose.yml` (user: postgres, password: secret, db: url_shortener)
- **Adminer**: Web UI at `localhost:8080` for database inspection
- **No authentication system** currently implemented (future extension point)

## Debugging Tips
- Check migration status: `dotnet ef migrations list`
- View generated SQL: Temporarily change EF Core log level in `Program.cs`
- Test redirects: Use `/Stats/{code}` to verify link creation before testing redirect
