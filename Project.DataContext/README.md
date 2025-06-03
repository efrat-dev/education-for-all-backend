# Project.DataContext

This project contains the Entity Framework Core data context and related configurations for managing the application's database.

## Contents

- `ApplicationDbContext.cs`  
  Main EF Core `DbContext` class implementing the `IContext` interface.

- `Migrations/`  
  Auto-generated folder containing EF Core migration snapshots and migration logic.

---

## ApplicationDbContext

The `ApplicationDbContext` class serves as the main bridge between the application and the PostgreSQL database.

### Features

- Uses `IConfiguration` for retrieving the connection string.
- Defines `DbSet<T>` properties for:
  - `User`
  - `Counselor`
  - `Topic`
  - `Post`
  - `DeleteTokenDto`
  - `RefreshTokenDto`
- Implements `Save()` method for saving changes asynchronously.
- Configures entity relationships and default values via Fluent API in `OnModelCreating`.
- Applies automatic UTC conversion to `DateTime` properties.

### Database Provider

- PostgreSQL via `UseNpgsql`.

---

## Migrations

The `Migrations` folder contains EF Core-generated code used to:

- Generate/update database schema.
- Maintain model snapshots.
- Configure table properties such as default values, foreign keys, and constraints.

---

## Fluent API Configuration Highlights

- **Post Entity**:
  - Relationships to `User` and `Counselor` with `Restrict` delete behavior.
  - `Date` property uses `NOW()` as default and stored in UTC.

- **Topic Entity**:
  - `DateCreated` uses `NOW()` as default and stored in UTC.
  - Foreign key relationship to `User`.

- **Join Tables**:
  - Many-to-many configuration for `Counselors` and `Topics`.

- **Token Entities**:
  - Used for managing authentication-related operations.

---

## Technologies

- **Entity Framework Core** 7.0
- **Npgsql** (PostgreSQL EF Core provider)
- **.NET 6/7** (depending on project setup)

---

## How to Use

1. Ensure your appsettings configuration contains the `DefaultConnection` string.
2. Register `ApplicationDbContext` in your DI container.
3. Use EF CLI commands like `dotnet ef migrations add` and `dotnet ef database update` for schema management.

---

## Notes

- All `DateTime` values are stored in UTC for consistency.
- Foreign keys are configured to avoid cascading deletes where necessary.
- Migration snapshots are auto-generated and should not be manually modified.

