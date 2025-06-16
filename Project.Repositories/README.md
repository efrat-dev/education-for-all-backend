# Project.Repositories

This folder contains the data access layer for the application. It is organized into entities, repository interfaces, and their implementations, enabling clean separation of concerns and ease of maintenance.

---

## 📁 Entities

Located in: `Project.Repositories/Entities`

These are the domain models that represent the structure of your database tables.  
Each entity uses standard data annotations and relationships.

### Entities:

- **User**: Represents a regular user with email, password, name, and related posts/topics.
- **Counselor**: Represents a counselor with extended profile details and relations to posts and topics.
- **Post**: A post within a topic, possibly created by either a user or a counselor.
- **Topic**: A discussion topic, always created by a user and may be associated with multiple counselors and posts.

---

## 📁 Interfaces

Located in: `Project.Repositories/Interfaces`

These interfaces define abstraction contracts for interacting with entities, allowing loose coupling and testability.

### Interfaces:

- **`IContext`**: Represents the abstraction for the EF Core context, exposing `DbSet`s and a `Save()` method.
- **`IForumRepository<T>`**: Generic repository interface for CRUD operations.
- **`IPostRepository`**: Post-specific operations (e.g., getting posts by topic).
- **`IUserRepository<T>`**: Generic user operations (with additional methods for checking existence by email/name).

---

## 📁 Repositories

Located in: `Project.Repositories/Repositories`

These are concrete implementations of the repository interfaces. They handle actual Entity Framework logic for each type.

### Implemented Repositories:

- **UserRepository**: Implements `IUserRepository<User>`.
- **CounselorRepository**: Implements `IUserRepository<Counselor>`.
- **PostRepository**: Implements `IPostRepository`.
- **TopicRepository**: Implements `IForumRepository<Topic>`.

Each repository handles common data access operations and some custom queries.

---

## 🛠️ Dependency Injection

Located in: `Project.Repositories/ServiceCollectionExtension.cs`

This class provides an extension method `AddRepository` that registers all repository services with the dependency injection container using scoped lifetime.

---

## 🧱 Design Principles

- **Entity-Repository Pattern**: Separates data logic from business logic.  
- **Interface-based Design**: Promotes flexibility and testability.  
- **Dependency Injection Ready**: Easily injectable into application services.  

---

## 📝 Notes

- `User` and `Counselor` are treated with separate generic repository implementations under a shared interface.  
- Posts support a polymorphic relationship (user or counselor).  
- Topics are only created by regular users, but counselors can be assigned to assist.
