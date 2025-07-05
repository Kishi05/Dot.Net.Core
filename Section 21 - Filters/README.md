# 📘 Section 21 – ASP.NET Core Filters

## 🚀 Module Goal

This section explores the complete filter pipeline in ASP.NET Core MVC — including how filters control execution flow, enforce logic at different scopes (Global, Controller, Action), and influence request/response behavior. It covers essential concepts like ordering, short-circuiting, exception handling, and creating custom filters using DI and factory patterns. Readers will understand not just how to use filters, but how to think in terms of the ASP.NET Core middleware lifecycle.

---

## Filter LIfecycle

       ┌───────────── Authorization Filter ──────────┐
       │   (Optional) Short-circuits if unauthorized │
       └─────────────────────────────────────────────┘
                        │
                        ▼
       ┌───────────── Authorization Filter ──────────┐
       │   (Optional) Short-circuits if unauthorized │
       └─────────────────────────────────────────────┘
                        │
                        ▼
       ┌────── Resource Filters (Before) ──────────┐
       │   (Optional) Auth, caching, metadata      │
       └───────────────────────────────────────────┘
                        │
                        ▼
        ┌───────── Model Binding & Validation ─────┐
        │   Forms route data into parameters       │        
        └──────────────────────────────────────────┘
        
                        │
                        ▼
      ┌──────────── Action Filters (Before) ───────┐
      │   Validate input, prep context             │
      └────────────────────────────────────────────┘
                        │
                        ▼
      ┌──────────── Controller Action Method ─────┐
      │   Executes the actual business logic      │
      └───────────────────────────────────────────┘
                        │
                        ▼
      ┌──────────── Action Filters (After) ───────┐
      │   Log, modify action result               │
      └───────────────────────────────────────────┘
                        │
                        ▼
     ┌──────────── Exception Filters ──────────────┐
     │   Trigger only on unhandled exceptions      │
     └─────────────────────────────────────────────┘
                        │
                        ▼
       ┌──────── Result Filters (Before) ─────────┐
       │   Shape or wrap response (e.g. headers)  │
       └──────────────────────────────────────────┘
                        │
                        ▼
     ┌──────────── IActionResult ──────────────────┐
     │   Razor, JSON, XML formatting               │
     └─────────────────────────────────────────────┘
                        │
                        ▼
       ┌──────────── Result Filters (After) ──────┐
       │   Shape or wrap response (e.g. headers)  │
       └──────────────────────────────────────────┘
                        │
                        ▼
       ┌──────────── Resource Filters (After) ─────┐
       │   (Optional) Auth, caching, metadata      │
       └───────────────────────────────────────────┘
                        │
                        ▼
         ┌────────── Response Sent ──────────┐
         └───────────────────────────────────┘

## Visual Pipeline (simplified)

```
app.UseRouting();
│
├── ① Logging Middleware
│     (Global logging, timing, correlation ID, etc.)
│
├── ② Authentication Middleware
│     (e.g., JWT bearer, cookie-based — sets HttpContext.User)
│
├── ③ Custom Middleware (e.g., Tenant Resolution)
│     (Reads headers, query strings, etc.)
│
└── MVC Endpoint Triggered
       ↓       
       ┌───────────────────────────────────────────┐       
       │         MVC Filter Pipeline               │       
       │                                           │
       │   - Authorization Filters                 │       
       │   - Resource Filters                      │       
       │   - Action Filters                        │       
       │   - Controller Method Executes            │       
       │   - Result Filters                        │       
       │   - Exception Filters                     │       
       └───────────────────────────────────────────┘
       
```

---

## 🧠 Key Concepts Covered

| Topic                       | Description                                                                 |
| --------------------------- | --------------------------------------------------------------------------- |
| Filter Pipeline             | Resource → Action → Result → Exception                                      |
| Scope + Order               | Global > Controller > Method (outer to inner)                               |
| IOrderedFilter              | Manually sets execution sequence using `Order` property                     |
| Short-circuiting            | Stops pipeline early using `context.Result = …`                             |
| Header Timing               | Verified when headers can/can’t be added pre/post `next()` call             |
| Skip Filter Pattern         | Lightweight opt-out via `SkipFilterAttribute`                               |
| IFilterFactory              | Dynamically create filters with parameters + DI (e.g., `[Audit("Create")]`) |
| ServiceFilter vs TypeFilter | DI-only vs Reflection + Param-injection                                     |
| ExceptionFilter Limitations | Doesn’t catch result-phase (e.g., View not found)                           |

---

## 🔑 Notes & Highlights

* Filters can be applied globally, per controller, or per method.
* **Global filters** are registered in `Startup.cs` via `AddService()` or `AddFilter()`.
* Filters **wrap the pipeline** in order — think of them like middleware rings.
* **IFilterFactory** is the best way to inject parameters and services inside an attribute.
* **Short-circuiting** can return results early by setting `context.Result` before calling `next()`.
* `SkipFilterAttribute` lets you dynamically bypass logic for specific endpoints.
* **Result Filters** must add headers before `await next()`.
* **Exception Filters** won't capture errors during view rendering.

---

## 🔁 Filter Execution Order Demo

```csharp
// Global filter registration (Order = 2)
options.Filters.AddService<OrderFilterAsyncActionFilter>(2);

// Controller-level filter (Order = 4)
[TypeFilter(typeof(OrderFilterAsyncActionFilter), Arguments = new object[] {4})]

// Method-level filter (Order = 1)
[TypeFilter(typeof(OrderFilterAsyncActionFilter), Arguments = new object[] {1})]
```

✅ **Actual Output:**

```
Order: 2 (Global - Before)
Order: 4 (Controller - Before)
Order: 1 (Method - Before)
Order: 1 (Method - After)
Order: 4 (Controller - After)
Order: 2 (Global - After)
```

---

## 🔒 SkipFilter Pattern

```csharp
public class SkipFilterAttribute : Attribute {}

if (context.Filters.Any(f => f is SkipFilterAttribute))
    return; // skip logic
```

> Helps selectively disable filters without code duplication.

---

## 🧾 Audit Filter with IFilterFactory

```csharp
[Audit("BLOCK")] // Will short-circuit
[Audit("Create")] // Will log
```

**AuditAttribute:** Implements `IFilterFactory` to pass tag param

```csharp
    public class AuditFactory : Attribute, IFilterFactory
    {
        private string _key { get; set; }
        public AuditFactory(string key) 
        {
            _key = key;
        }
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            HomeArgsFilter? instance = serviceProvider.GetService<HomeArgsFilter>();
            instance.Key = _key;
            return instance;
        }
    }
```

**AuditFilter:** Inherits `ActionFilterAttribute`

```csharp
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_key == "BLOCK")
            {
                context.Result = new ContentResult { Content = "Blocked", StatusCode = 403 };
                return;
            }

            _logger.LogInformation("Before : IFactoryFilter - {FileName}.{MethodName}", nameof(HomeArgsFilter), nameof(OnActionExecutionAsync));
```

---

## 🔍 Middleware vs. Filters

| 🔍 Aspect                        | Middleware                                           | MVC Filters                                               |
| -------------------------------- | ---------------------------------------------------- | --------------------------------------------------------- |
| 📍 Scope                         | Global (entire app pipeline)                         | MVC only (Controller/Action lifecycle)                    |
| 🧬 Lives Where?                  | Registered in `Program.cs` with `app.Use...()`       | Added globally or via attributes (`[Authorize]`, etc.)    |
| 🎯 Use Cases                     | Auth, logging, error handling, headers, CORS         | Model validation, auditing, result formatting             |
| 📌 Runs Before Routing?          | ✅ Yes                                                | ❌ No – executes only after routing                        |
| 🧪 Access to Route Data / Model? | ❌ No – only `HttpContext`                            | ✅ Yes – has full access to controller, action args, model |
| 🎭 AuthZ Layer                   | AuthN (e.g., JWT, cookies) via middleware            | AuthZ (roles/policies) via `IAuthorizationFilter`         |
| 🧼 Can Short-Circuit Request?    | ✅ Yes – skip `_next()` and write response            | ✅ Yes – set `context.Result` without calling `next()`     |
| 🔄 Wraps What?                   | Entire pipeline (e.g., Static files, API, MVC, etc.) | Only the action/result pipeline inside MVC                |
| 🛠️ Reusable?                    | ✅ Highly reusable, even in non-MVC apps              | Tied to MVC pipeline                                      |
| 🧠 Can Inject Services?          | ✅ Yes – via constructor injection                    | ✅ Yes – via `[TypeFilter]`, `[ServiceFilter]`, or factory |
| 🧯 Global Error Handling?        | ✅ Use `UseExceptionHandler` / custom middleware      | ✅ ExceptionFilter for unhandled MVC action errors         |
| 🔍 Sees View Errors?             | ✅ Yes                                                | ❌ No – ExceptionFilter can’t catch Razor rendering errors |
| 🧠 Knows Controller Metadata?    | ❌ No                                                 | ✅ Yes – has access to action, controller, etc.            |
| 🔁 Executes Per Request?         | ✅ Once (unless re-entered)                           | ✅ Per matching MVC action, in order                       |
| 🧮 Ordering Mechanism            | Top-down order of `app.Use*()` calls                 | `Order` property + Scope: Global → Controller → Method    |
| 🧠 Best For                      | Framework-agnostic logic                             | MVC-specific lifecycle control                            |


---

## ⚠️ Gotchas You Caught

* ✅ **Result filter headers** must be added *before* `next()` or they won't apply.
* 🚫 **Exception filters don’t fire** on view rendering failures.

---

## 📌 Flash Cards

1. **What is `IFilterFactory` used for?**

   * To dynamically create filters with parameters while still injecting dependencies.
2. **Difference between `[TypeFilter]` and `[ServiceFilter]`?**

   * TypeFilter supports constructor arguments; ServiceFilter pulls from DI container.
3. **How to short-circuit an action filter?**

   * Set `context.Result = new ContentResult()` before calling `next()`.
4. **Can ExceptionFilters catch View errors?**

   * No, those errors occur in the result phase, beyond ExceptionFilter scope.
5. **When should headers be written in Result filters?**

   * Always *before* `await next()` to ensure they are applied.

---
