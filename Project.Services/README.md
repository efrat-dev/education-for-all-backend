# 🧩 Project.Services

This folder is responsible for defining and registering the service layer of the application. It connects data access layers (repositories) with business logic, and manages application-wide service registration through dependency injection.

---

## 📄 MapProfile.cs

- Configures object mappings using **AutoMapper**.
- Provides bi-directional mapping between Entity classes and their corresponding DTOs.
- Supports mapping for both single objects and asynchronous `Task<T>` and `List<T>` types.

## 📄 ServiceCollectionExtension.cs

- Contains an extension method `AddServices` for registering application services into the `IServiceCollection`.
- Adds services such as user management, forum handling, email, token, login, registration, and topic reading.
- Includes dependency injection for interface-to-implementation mappings.
- Registers the `MapProfile` AutoMapper configuration.

---

## 📁 Services

This subdirectory contains the **concrete implementations** of the service interfaces. These classes encapsulate business logic and act as the core of the application's behavior.

Services typically:
- Handle validation and authorization.
- Invoke repository methods.
- Coordinate between multiple entities or actions.
- Apply business rules and workflows.

Each service implements one or more interfaces from the `Interfaces` folder.

---

## 📁 Interfaces

This subdirectory defines **service interfaces** that abstract the business logic layer.

Interfaces:
- Enable loose coupling and better testability.
- Are used for dependency injection to promote flexibility and separation of concerns.
- Include contracts for services such as user management, forum post handling, token operations, authentication, and more.

---

## 🛠️ Technologies Used

- **AutoMapper** for model mapping.
- **Microsoft.Extensions.DependencyInjection** for service registration.
- **Scoped services** for lifetime control within requests.

---

This structure ensures that services are modular, testable, and cleanly separated from both the presentation and data access layers.
