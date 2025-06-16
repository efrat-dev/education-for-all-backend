# 📦 Common1

This directory serves as a shared library for data transfer objects (DTOs) used across multiple layers of the application. Its purpose is to maintain consistency and reusability of core data structures.

## 📁 Subdirectory: Dto

The `Dto` subdirectory contains multiple C# classes that act as DTOs (Data Transfer Objects). These classes define the structure of data exchanged between components or layers of the system, such as API controllers, services, and repositories.

### Key Characteristics

- Belongs to the `Common.Dto` namespace.
- Focuses solely on data structure — contains no business logic.
- Facilitates serialization, deserialization, and input validation.

### Overview of Included DTOs

- DTO for user contact requests.
- Extended DTO for contacting counselors.
- DTO representing counselor profiles and personal details.
- DTO for deleting tokens tied to posts.
- DTO for user login operations.
- DTO representing post metadata and ownership.
- DTO for managing refresh tokens.
- DTO describing discussion topics.
- DTO for user account details.

## ✅ Purpose

- Promote **modularity** and **code reuse**.
- Ensure **consistent data contracts** across microservices or layers.
- Simplify **unit testing** and **validation workflows** by separating data representation.

## 🛠️ Usage

Import and use the DTOs in your services, controllers, or other layers where data needs to be transferred without exposing internal domain models.

---

*This folder is intended for internal application use and should not contain business logic or service dependencies.*
