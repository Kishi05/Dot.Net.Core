# 🗄️ Entity Framework Core Integration

This module demonstrates how to integrate **Entity Framework Core** using a real SQL Server database. It replaces the in-memory service layer from earlier sections with full database-backed persistence using EF Core.

---

## 📚 What This Section Covers

- Installing and configuring EF Core
- Setting up `DbContext` and mapping entities
- Configuring `DbContext` in `Program.cs`
- Managing database connections via `appsettings.json`
- Applying migrations to create/update the DB schema
- Seeding static data using `HasData`
- Executing CRUD operations via EF Core
- Auto-generating identity keys
- Handling special cases like read-only/dummy records

---

## 🏗️ Architecture Overview

| Layer             | Role                                              |
|------------------|---------------------------------------------------|
| `Admin_User`      | The domain entity mapped to a database table     |
| `NetCoreAppDBContext` | EF Core `DbContext` managing Users and configuration |
| `appsettings.json` | Holds the SQL Server connection string          |
| `Program.cs`      | Registers `DbContext` in the service container   |

---

## 🔧 Setup & Configuration

1. **Install EF Core NuGet packages** in both projects (Web + Class Library):
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```
   
2.  Configure your DbContext in Program.cs:
	```
	builder.Services.AddDbContext<NetCoreAppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
	```
 
	 > ⚠️ **Note:**  
	> This line is specifically used to suppress the `PendingModelChangesWarning`, which occurs when `HasData()` includes dynamic values like `DateTime.UtcNow` or `Guid.NewGuid()`.
	>
	> Ideally, EF Core seed data should use **static values** to ensure consistent and repeatable migrations.
	>
	> In this project, we intentionally use dynamic values for `CreatedOn` and `ModifiedOn`, as they are meant to reflect runtime timestamps. To explore and test this behavior, we’ve added the following suppression:
	>
	> ```csharp
	> .ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
	> ```
	>
	> ⚠️ **Use with caution in production** — this only hides the warning; it does **not** fix the underlying model drift.

3. Add connection string in appsettings.json:
	``` Dummy
	"ConnectionStrings": {
	  "Default": "Server=.;Database=NetCoreAppDB;Trusted_Connection=True;"
	}
	```
 
4. Apply migrations and update DB:
	```
	Add-Migration Initial
	Update-Database
	```

---

### 🧬 Data Model & Seeding

- Entity: Admin_User is mapped to a SQL Server table via DbSet<Admin_User>.
- Primary Key: Id is auto-generated using IDENTITY(1001,1).
- Seeding: One dummy user is inserted via HasData() with Id = 1.
- Special Field: IsProtected (formerly IsDummy) prevents editing/deleting of seeded rows.

---

### ✅ Features Implemented
- Full database integration
- Auto-incrementing identity for real users (starting from 1001)
- Seamless model binding from ViewModel → Entity → DB
- Server-side validation with database persistence
- Real-time feedback using TempData post-save
- Delete protection for dummy/system records

----

### 🛠️ Tools & Technologies

- EF Core 8
- SQL Server
- ASP.NET Core MVC
- Code-first Migrations
- Dependency Injection
- Tailwind CSS for UI consistency

---

### 🧪 Useful EF Core Commands

| Command                 | Purpose                                     |
| ----------------------- | ------------------------------------------- |
| `Add-Migration Initial` | Creates the first migration snapshot        |
| `Update-Database`       | Applies latest migrations to DB             |
| `Script-Migration`      | Generates SQL for pending migrations        |
| `Remove-Migration`      | Reverts the last migration (if not applied) |
| `Drop-Database`         | Deletes the current DB (dev-only!)          |

---

# Key Learnings

- EF Core allows clean persistence using a repository-like DbContext.
- Seeding requires fixed keys – you can’t leave Id as null in HasData().
- SQL Server’s identity auto-increment is smart enough to ignore lower explicit inserts (e.g., dummy Id = 1).
- Using AsNoTracking() improves performance for read-only queries.
- DbContext.SaveChanges() populates auto-generated Id values automatically.

>### ⚠️ Note:
>- EF Core with Relationships and Async are yet to be integrated.
