# 📘 Swagger / Open API, XML Comments & Versioning


## 🚀 Module Objectives
1. **Generate Open API (Swagger) spec** automatically from controllers & DTOs.  
2. **Serve Swagger UI** at `/swagger` for interactive testing.  
3. **Include XML comments** so endpoint docs, parameter descriptions, and response codes appear in UI.  
4. **Add API Versioning** (`v1`, `v2`, …) that flows into routes *and* Swagger docs.

---

## 🛠 Setup Steps

### 1. Add NuGet packages
```bash
dotnet add package Swashbuckle.AspNetCore --version 7.0.14
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
```

---

### 🧠 Flash Cards
Where does Swagger get its schema from?
– Reflection over controllers + ApiExplorer + attribute metadata.

Why prefer XML comments over attributes for docs?
– Keeps descriptive text out of code, centralizes docs, supports <example> tags.

How does versioning integrate with Swagger?
– ApiExplorer groups endpoints by ApiVersionDescription; each group generates its own Swagger document.

What happens if you omit AssumeDefaultVersionWhenUnspecified?
– Requests without api/v{version} return HTTP 400.