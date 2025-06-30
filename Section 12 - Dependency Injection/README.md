# 🧩 ASP.NET Core Dependency Injection Demo

This project demonstrates the core concepts and implementation of **Dependency Injection (DI)**. It covers injecting services, configuring lifetimes, and promoting loose coupling between components.

---

## 📌 What is Dependency Injection?

**Dependency Injection (DI)** is a design pattern used to achieve **Inversion of Control (IoC)** between classes and their dependencies. Instead of a class creating its own dependencies, they are **provided from the outside**, usually by a framework like ASP.NET Core's built-in DI container.

---

## 💡 Why DI Matters

- ✅ Improves **testability** – easily inject mocks or fakes
- ✅ Encourages **loose coupling**
- ✅ Supports **separation of concerns**
- ✅ Built-in support in ASP.NET Core (no third-party tools required)

---

## ⚙️ Service Lifetimes

| Lifetime      | Description                                                                 |
|---------------|-----------------------------------------------------------------------------|
| `Singleton`   | One instance for the entire application lifetime                            |
| `Scoped`      | One instance per request                                                    |
| `Transient`   | A new instance every time it's requested                                    |

**Example:**
```csharp
builder.Services.AddSingleton<IMyService, MyService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddTransient<ILogService, ConsoleLogService>();
```
### 💬 Can You Inject Into Non-Controller Classes?

Yes! ASP.NET Core's built-in Dependency Injection system allows injecting services not only into controllers but also into various other framework components.

**✅ Supported Injection Targets:**

- Razor Pages and MVC Controllers  
- View Components  
- Middleware  
- Custom Services and Helpers  
- Background Services (`IHostedService`, etc.)  
- Tag Helpers & Filters  

> 💡 Tip: You can also use `[FromServices]` attribute for parameter injection inside action methods.

---

### 🧰 Constructor Injection vs Method/Property Injection

| **Type**            | **Description**                                                                 |
|---------------------|---------------------------------------------------------------------------------|
| **Constructor**     | Most common and preferred. Ensures all required dependencies are provided during object creation. Promotes immutability and better testability. |
| **Method/Property** | Used for optional or late-binding dependencies. Less commonly used in ASP.NET Core.    |

> ✅ **Constructor injection** is safer and preferred when dependencies are required for the object to function correctly.

