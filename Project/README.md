# ASP.NET Core Web API – Project Documentation

## Overview

This project is a RESTful Web API built with **ASP.NET Core** that includes full **JWT authentication**, **CORS configuration**, and **Swagger integration** for documentation. The API serves endpoints for handling authentication, user and counselor management, forum topics, posts, and contact functionality. It is designed to work efficiently with a frontend application (e.g., on `localhost:3000`) and can be deployed to platforms like **Render Cloud**.

---

## Features

- 🔒 JWT-based Authentication
- 📬 Email-based Contact & Report Services
- 🧑‍💼 User and Counselor Management
- 🧵 Forum Topics and Posts Management
- ✅ Swagger UI for API Testing
- 🌍 CORS configuration with environment-based origin control
- ☁️ Ready for deployment to cloud platforms (e.g., Render)
  
---

## Configuration Notes

### 🛡️ Authentication
Uses JWT Bearer tokens with custom configuration for:
- Issuer
- Audience
- Symmetric signing key (from appsettings)

### 🌐 CORS Policy
Allows specified origins from configuration (`AllowedOrigins`) and exposes certain headers.

### 📄 Swagger
Swagger is configured with support for JWT authentication headers to test protected routes easily.

### 💾 Dependency Injection
- Services and Repositories are registered via custom extension methods.
- Uses `ApplicationDbContext` for Entity Framework Core.
