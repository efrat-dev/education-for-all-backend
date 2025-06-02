# 📁 Project.Repositories/Interfaces

This folder contains core **repository interface definitions** used for abstracting data access operations within the project. These interfaces allow the application to interact with the database in a clean, consistent, and testable manner.

## ✅ Purpose

- Provide **contracts** for repository implementations.
- Enable **dependency injection** for easier testing and maintainability.
- Support both **generic** and **entity-specific** data operations.

## 📄 Interfaces Overview

### `IContext`
An abstraction over the Entity Framework DbContext. It exposes the main entity sets used by the application and includes a method to persist changes.

### `IForumRepository<T>`
A generic interface defining basic CRUD operations for forum-related entities such as posts and topics.

### `IPostRepository`
An extension of `IForumRepository` specialized for working with `Post` entities, adding methods tailored to post-related queries.

### `IUserRepository<T>`
A user-centric interface providing operations for managing user entities, as well as validations like checking for existing emails or usernames.

## 🧩 Design Principles

- **Separation of concerns**: These interfaces decouple data logic from the rest of the application.
- **Generic programming**: Promotes reusability through type parameterization.
- **Extensibility**: Custom repositories extend base interfaces for more specific functionality.

---

This structure ensures that repository implementations remain consistent and testable across the project.
