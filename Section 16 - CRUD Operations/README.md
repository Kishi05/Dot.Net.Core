# CRUD Operations Demo – ASP.NET Core MVC

A sample application that demonstrates clean CRUD workflows in **ASP.NET Core MVC** using an **in‑memory data store**. Ideal for workshops, or as a foundation for more advanced labs.

---

## ✨ Key Highlights

* **Instant‑start** – no database, no external dependencies. Clone & run.
* **Service class library** – business logic is extracted to a separate project and injected into the Web project using dependency injection.
* **Singleton service layer** – users are stored in a single in‑memory list for the app's lifetime.
* **Registration + Search** – two focused pages that cover create, read, update, and delete scenarios.
* **Dummy record** – the app ships with one placeholder user (marked `IsDummy = true`). It appears in the grid but cannot be edited or deleted, showcasing conditional UI states.
* **Robust validation** – model‑level data‑annotations surface through a styled validation summary.
* **Clear user feedback** – success and error banners driven by `TempData`, together with the Post‑Redirect‑Get pattern for a smooth UX.

---

## 🖼️ Application Flow

1. **Landing / Search** – users arrive at the Search page and see the grid of users. *(Note: no filter/search logic is implemented yet — grid displays all users.)*
2. **Add User** – the *Add User* button opens the Registration page in *create* mode.
3. **Edit User** – selecting *Edit* on any non‑dummy row re‑uses the same Registration view, pre‑populated.
4. **Save** – on submission, server‑side validation runs:
   * **Invalid data** → the page is redisplayed with a red validation summary.
   * **Valid data** → the record is stored in the singleton list, `TempData["Success"]` is set, and the app redirects to `GET /Registration/{id}` so the user lands back on the form, now showing the saved values plus a green success banner.
5. **Delete** – removes the selected user unless it is the dummy entry; UI disables the action for the dummy row.

---

## ⚙️ Architectural Notes

| Layer              | Responsibility                                                                                   |
|--------------------|--------------------------------------------------------------------------------------------------|
| **Presentation**   | Razor views for Search (grid) and Registration (form). Tailwind CSS provides modern styling.     |
| **Controller**     | Orchestrates requests, handles validation, sets `TempData`, and delegates work to the service.   |
| **Service Layer**  | Extracted into a **separate class library** and registered in DI as a singleton. Holds user list. |
| **Model & ViewModel** | `User` represents the domain; `UserViewModel` carries validation and is mapped manually.      |

> **Why a singleton?** It mimics a simple repository without introducing a database, letting you focus purely on MVC patterns.

---

## 🛠️ Technology Stack

* .NET 8  
* ASP.NET Core MVC  
* Razor Views  
* Tailwind CSS (via CDN) for styling  
* Built‑in Dependency Injection  
* C# 12 features (nullable reference types enabled)

---

## 🚀 Getting Started

1. **Clone the repository**
2. Ensure `.NET 8 SDK` is installed.
3. From the solution directory, run:
   ```bash
   dotnet run```

---

## 📝 Validation & Feedback Design

* Annotations – [Required], [StringLength], etc., drive server‑side validation.
* Validation Summary – errors are grouped in a single red banner for high visibility.
* Success / Error banners – green or red alerts appear based on TempData after each operation.

---

## 📦 Potential Next Steps

| Enhancement              | Rationale                                                     |
| ------------------------ | ------------------------------------------------------------- |
| SQLite or SQL Server DB  | Persist data across restarts and showcase EF Core.            |
| AutoMapper               | Remove manual mapping logic between entities and view-models. |
| Pagination & Sorting     | Scale the grid for larger datasets.                           |
| Filtering / Search Logic | Enhance the landing page usability.                           |
| Unit & Integration Tests | Demonstrate testable architecture with xUnit.                 |
