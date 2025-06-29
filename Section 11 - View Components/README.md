# 🔧 ViewComponent

This project demonstrates the usage of **View Components**  — a powerful, reusable way to encapsulate rendering logic and UI, much like partial views, but with full controller-like capabilities.

---

## 🚀 What is a ViewComponent?

A **ViewComponent** is a mini controller + view combo that allows you to:

- Render reusable UI blocks with logic
- Execute logic outside of a full controller/action cycle
- Improve separation of concerns in MVC applications
- Return HTML directly to a view or layout

Unlike partial views, ViewComponents support complex logic and **dependency injection**, making them ideal for widgets, menus, data panels, etc.

---

## 💡 Key Concepts

### ✅ Why use View Components over Partial Views?
| Partial View                      | View Component                            |
|----------------------------------|--------------------------------------------|
| Renders a UI fragment only       | Combines UI + logic like a mini controller |
| No built-in logic support        | Can include C# logic (e.g. services, DI)   |
| Must pass data manually          | ViewComponent handles its own data         |
| Good for static/snippet content  | Great for dynamic or logic-based UI        |

---

## 🧠 Important Details

- ViewComponent classes must end with ViewComponent or be explicitly referenced.
- Views must be placed in /Views/Shared/Components/{ComponentName}/Default.cshtml.
- ViewComponents support constructor injection (e.g., services, DB contexts).
- Use **Invoke()** or **InvokeAsync()** methods for sync or async rendering.
