# 📁 Project.Repositories/Repositories

This folder contains the **concrete repository implementations** responsible for performing data operations on various entities using Entity Framework Core.

## ✅ Purpose

These classes implement the interfaces defined under `Project.Repositories/Interfaces` and provide the actual logic for interacting with the database via `IContext`.

## 📄 Repository Classes Overview

### `UserRepository`
Handles CRUD operations for `User` entities, as well as checks for existing email and username. Implements `IUserRepository<User>`.

### `CounselorRepository`
Manages data access for `Counselor` entities, using the generic `IUserRepository<Counselor>` interface.

### `PostRepository`
Implements `IPostRepository` to handle operations related to `Post` entities, including fetching posts by topic.

### `TopicRepository`
Provides data access for `Topic` entities. Implements `IForumRepository<Topic>` and includes logic for eager-loading related `Posts` and `Counselors`.

## 🧩 Implementation Details

- Each class receives an instance of `IContext` via dependency injection.
- All operations are asynchronous to support non-blocking I/O.
- The repositories abstract away the database access layer and allow the application to remain loosely coupled and testable.

## 📦 Technologies Used

- **Entity Framework Core** for ORM and asynchronous database operations.
- **C# interfaces and generics** to promote code reuse and maintainability.

---

These implementations form the backbone of the data access layer, enabling clean interaction between the domain logic and persistent storage.
