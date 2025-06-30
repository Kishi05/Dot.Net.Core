# ⚙️ Order of Precedence of Configuration Sources in ASP.NET Core

ASP.NET Core loads configuration settings in a specific **order of precedence**.  
When multiple sources define the same key, **the last one wins**.

---

## 📊 Configuration Precedence (Low ➡️ High)

| Priority | Source | Description |
|----------|--------|-------------|
| 1️⃣ | `appsettings.json` | Default/base configuration file |
| 2️⃣ | `appsettings.{Environment}.json` | Environment-specific overrides (e.g., `appsettings.Development.json`) |
| 3️⃣ | **User Secrets** | Local development secrets (via Secret Manager) |
| 4️⃣ | **Environment Variables** | Set in OS, CI/CD, Docker, Azure, etc. |
| 5️⃣ | **Command-line Arguments** | Highest priority (`dotnet run --SettingKey=value`) |

---

## ✅ Practical Use Cases

- Use `appsettings.json` for defaults shared across environments.
- Use `appsettings.Production.json` to override production settings.
- Use **User Secrets** for API keys or passwords during development.
- Use **Environment Variables** to manage secrets securely in cloud hosting.
- Use **Command-line arguments** in CI pipelines or local testing overrides.

---

## 🔍 Debug Tip

To inspect active config sources:

```csharp
foreach (var item in config.AsEnumerable())
{
    Console.WriteLine($"{item.Key} = {item.Value}");
}
```
or

```
IConfigurationSection section = config.GetSection("YourKey");
Console.WriteLine(section.Value);
```
---

### 🛠️ Example Command-Line Override

```
dotnet run --AppSettings:ConnectionString="Server=prod;Database=LiveDB"
```

---

⚠️ If two sources define the same key, the last one loaded wins.
It's common to have a setting defined in 3+ places — ensure you're overriding with intent.