# `Project.Services`

This folder contains the core service classes responsible for implementing the business logic of the application.  
Each service adheres to clean architecture principles, using dependency injection, separation of concerns, and asynchronous programming for performance and testability.

---

## 🧩 Available Services

### `CounselorService`
Provides CRUD operations and validation logic for counselor accounts.

- **Implements**: `IUserService<CounselorDto>`
- **Dependencies**:
  - `IUserRepository<Counselor>`
  - `IMapper` (AutoMapper)
- **Key Methods**:
  - `AddAsync`
  - `GetByIdAsync`
  - `GetAllAsync`
  - `UpdateAsync`
  - `DeleteByIdAsync`
  - `EmailExists`
  - `UserNameExists`

---

### `DeleteTokenService`
Manages lifecycle of delete confirmation tokens used for post deletions.

- **Implements**: `IDeleteTokenService`
- **Storage**: Uses `ApplicationDbContext` for persistence
- **Key Methods**:
  - `GenerateTokenAsync`
  - `GetTokenAsync`
  - `DeleteTokenAsync`

---

### `EmailService`
Handles outbound email communication using SMTP (MailKit).

- **Implements**: `IEmailService`
- **Configuration**: Loaded via `IConfiguration`
- **Supported Scenarios**:
  - `SendEmailAsync`: General purpose
  - `SendReportEmailAsync`: Reports to admins
  - `SendContactCounselorEmailAsync`: Site user → counselor
  - `SendContactEmailAsync`: Site user → manager

---

### `LoginService`
Responsible for authentication, token issuance, and session lifecycle management.

- **Implements**: `ILoginService`
- **Dependencies**:
  - `IUserService<UserDto>`
  - `IUserService<CounselorDto>`
  - `ITokenService`
  - `IHttpContextAccessor`
  - `IConfiguration`
- **Key Responsibilities**:
  - Authenticate users and counselors
  - Generate JWT and refresh tokens
  - Extract role and user ID from tokens
  - Store tokens in secure HTTP-only cookies
  - Revoke tokens and logout users

---

### `PostService`
Orchestrates all post-related logic, from persistence to notifications.

- **Implements**: `IPostService`
- **Dependencies**:
  - `IPostRepository`
  - `IMapper` (AutoMapper)
  - `IEmailService`
  - `IConfiguration`
- **Key Features**:
  - Full CRUD support
  - Email alerts on new posts
  - DTO ↔ Entity mapping via AutoMapper

---

### `RegistrationService`
Coordinates the registration flow for users and counselors.

- **Implements**: `IRegistrationService`
- **Key Features**:
  - `RegisterUserAsync`: Registers a new user
  - `RegisterCounselorAsync`: Registers a new counselor
  - Email and username conflict resolution
  - Password hashing via BCrypt
- **Design Notes**:
  - Uses `addCallback` pattern for clean persistence injection
  - Validates using `IUserService<T>` interfaces

---

### `TokenService`
Handles secure token management for login sessions.

- **Implements**: `ITokenService`
- **Key Responsibilities**:
  - `GenerateAccessToken`: Short-lived JWT with role/ID claims
  - `GenerateRefreshTokenAsync`: Secure token generation & DB storage
  - `ValidateRefreshTokenAsync`: Token verification and user resolution
  - `RevokeRefreshTokenAsync`: Invalidates the refresh token
- **Security**:
  - JWT signed with `HmacSha256`
  - Refresh tokens expire and can be revoked
  - Role/user ID resolved via DB lookups in `Users` or `Counselors`

---

## ⚙️ General Notes

- All services follow asynchronous patterns (`Task<T>`) to ensure scalability.
- Designed with **testability** and **modularity** in mind.
- Configuration (especially for email, tokens) should be managed via `appsettings.json` or environment variables.
- Codebase encourages **interface-based abstraction**, **dependency injection**, and **single responsibility** per service.
