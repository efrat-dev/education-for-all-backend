# Education for All – Backend

This is the backend of the **Education for All** platform — a secure, role-based forum designed to facilitate communication between parents and educational counselors.
Built using **C#** and **ASP.NET Core Web API**, this server handles user authentication, question and answer management, counselor workflows, and integration with speech services and email notifications.

## 🌐 Overview

- Enables parents to post questions and counselors to respond
- Provides secure, role-based access for different types of users
- Integrates voice-based features and email services
- Built with a modular, scalable architecture and modern design patterns

## ⚙️ Tech Stack

- **C#**, **.NET 6**, **ASP.NET Core Web API**
- **Entity Framework Core** (code-first, SQL Server)
- **SQL Server** via **SSMS** (SQL Server Management Studio)
- **Azure Speech Services** for Text-to-Speech (TTS)
- **MailKit** for SMTP-based email communication
- **JWT-based Authentication** with HTTP-only refresh tokens
- **RBAC (Role-Based Access Control)** for permission management

## 🔐 Security & Authentication

- **Password hashing** using ASP.NET Identity cryptographic tools
- **JWT Access Tokens** for secure stateless authentication
- **Refresh Tokens** stored in **HTTP-only secure cookies**
- **RBAC**: Role-based access for endpoints (e.g., parent, counselor)
- Input validation and safe data handling throughout

## 📬 Communication Features

- **Email Services** using **SMTP** via **MailKit**
  
## 🧩 Design Patterns & Architecture

- **Dependency Injection** (built-in ASP.NET Core container)
- **Repository Pattern** for data abstraction
- **Singleton Services** for shared state where applicable
- **Layered Architecture**:
  - Controllers → Services → Repositories → DB Context
    
## 📂 Main Features

- 🔐 **User Authentication**: Register/login with secure token handling  
- 🧑‍🏫 **Counselor Role**: Can answer user-submitted questions  
- 👪 **Parent Role**: Can ask questions and view responses  
- 🔄 **Token Refresh Flow**: Automatic access token renewal via secure cookie  
- ✉️ **Email Notifications**: Account verification and activity alerts  
- 🗣️ **Text-to-Speech Integration**: Azure-based TTS service (for accessibility)  

## 🔗 Related Repositories

- [education-for-all-frontend](https://github.com/efchbd1013/education-for-all-frontend)

## 🛠️ Setup Instructions

### 1. Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [SQL Server + SSMS](https://learn.microsoft.com/en-us/sql/ssms)
- **Azure Speech Services** key (for Text-to-Speech and Speech-to-Text support)
- **Mail server credentials** (for SMTP email)

### 2. Running the Project Locally

1. Clone the repository:
    ```bash
    git clone https://github.com/efchbd1013/education-for-all-backend.git
    cd education-for-all-backend
    ```

2. Set up your database connection in `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=EducationDb;Trusted_Connection=True;"
    }
    ```

3. Configure your secrets (in `appsettings.json` or user-secrets):
    - **JWT secret**
    - **SMTP credentials**
    - **Azure Speech key**

4. Apply migrations and run the project:
    ```bash
    dotnet ef database update
    dotnet run
    ```
    
The API should now be running on the port displayed in the console output.

### 3. `appsettings.json` (Structure Example)
**Note**: This is a template example of how the `appsettings.json` file should be structured. Be sure to replace the placeholder values with your actual sensitive information, such as API keys, SMTP credentials, and JWT secrets.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "YourSecureKeyHere", 
    "Issuer": "http://localhost:YOUR_PORT",
    "Audience": "http://localhost:YOUR_PORT"
  },
  "EmailSettings": {
    "SiteManagerEmail": "your-email@example.com",
    "SiteManagerName": "Site Manager Name",
    "FromName": "Your Application Name",
    "FromEmail": "your-sender-email@example.com",
    "SmtpServer": "smtp.example.com",
    "SmtpPort": 587,
    "SmtpUser": "your-email@example.com",
    "SmtpPass": "YourSmtpPasswordHere"
  },
  "AzureSpeech": {
    "Key": "YourAzureSpeechKeyHere",
    "Region": "your-region"
  },
  "AllowedOrigins": "http://localhost:3000,http://your-frontend-domain.com",
  "ASPNETCORE_ENVIRONMENT": "Development"
}
