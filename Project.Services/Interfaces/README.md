# 📂 Project.Services/Interfaces

This folder defines **service interfaces** that establish the contract for business logic operations within the application. These interfaces are implemented by service classes in the `Services` folder, and are registered for Dependency Injection to enable loose coupling and testability.

---

## 📄 Overview of Interfaces

### 🔐 ILoginService
Handles user authentication logic including login, token refreshing, and logout.

### 📨 IEmailService
Responsible for sending various types of emails such as registration, contact, and report emails.

### 📮 IPostService
Extends `IForumService<PostDto>` and includes additional methods for post-related actions like liking a post, fetching posts by topic, and reporting.

### 🧾 IForumService<T>
Generic CRUD interface for forum entities (e.g., topics, posts). Includes:
- `GetByIdAsync`
- `GetAllAsync`
- `AddAsync`
- `UpdateAsync`
- `DeleteByIdAsync`

### 👥 IUserService<T>
Generic CRUD interface for user entities (e.g., regular users, counselors). Also includes methods for checking if an email or username already exists.

### 🎫 IRegistrationService
Handles logic for registering users and counselors with optional callbacks for custom post-registration behavior.

### 🧠 ITopicReadingService
Tracks topic reading activity to support features such as unread indicators or analytics.

### 🗝️ ITokenService
Manages token generation and validation, including access tokens and refresh tokens.

### ❌ IDeleteTokenService
Manages deletion token generation and validation, typically used for secure deletion of sensitive resources.

---

## 🧩 Purpose

The `Interfaces` directory serves to:
- Abstract business logic for flexibility and testing.
- Define contracts clearly between layers.
- Support scalable, modular architecture with dependency injection.

Each interface is implemented in the `Services` folder to provide the actual logic while maintaining separation of concerns.

---

## ✅ Best Practices

- Keep interfaces lean and focused on use-case responsibilities.
- Use generic types where applicable (`IUserService<T>`, `IForumService<T>`).
- Prefer asynchronous operations to ensure scalability.

---

This structure aligns with SOLID principles and supports maintainable, testable code across the service layer.
