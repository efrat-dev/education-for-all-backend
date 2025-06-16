# 🗂️ Project.Repositories/Entities

This folder contains **Entity Framework Core** entity classes representing the domain models and their relationships as stored in the database. These entities are mapped to the corresponding database tables and used by the data access layer (repositories).

---

## 📄 Entity Classes

### 👤 `User`
Represents a basic user who can open topics and create posts.

- **Fields:**
  - `Id`, `Name`, `Email`, `Password`
- **Navigation Properties:**
  - `Topics`: Topics created by the user.
  - `Posts`: Posts written by the user.

---

### 🎓 `Counselor`
Represents a counselor with extended profile and permissions.

- **Fields:**
  - `Id`, `Name`, `Email`, `Password`, `IdentityNumber`, `PhoneNumber`
  - `Bio`, `YearsOfExperience`, `EducationalInstitutions`, `WorkHistory`, `AcademicDegrees`
- **Navigation Properties:**
  - `Topics`: Topics the counselor is assigned to.
  - `Posts`: Posts written by the counselor.

---

### 📝 `Post`
Represents a post in a topic, written by a user or counselor.

- **Fields:**
  - `Id`, `Date`, `Content`, `Likes`, `IsReported`, `IsDeleted`
- **Foreign Keys:**
  - `TopicId`: Links to the topic.
  - `UserId`: Optional link to the user who posted.
  - `CounselorId`: Optional link to the counselor who posted.
- **Navigation Properties:**
  - `Topic`, `User`, `Counselor`

---

### 🧵 `Topic`
Represents a discussion topic opened by a user.

- **Fields:**
  - `Id`, `Title`, `WasAnswered`, `DateCreated`
  - `DateLastActive` (calculated, not mapped)
- **Foreign Keys:**
  - `UserId`: The user who opened the topic.
- **Navigation Properties:**
  - `User`, `Counselors`, `Posts`

---

## 🔄 Relationships Summary

- A **User** can open many **Topics** and write many **Posts**.
- A **Counselor** can write many **Posts** and be assigned to many **Topics**.
- A **Topic** has many **Posts**, is opened by a **User**, and can be linked to multiple **Counselors**.
- A **Post** belongs to a **Topic**, and is written by either a **User** or a **Counselor**.

---

## 🧩 Notes

- Entity navigation is set up using `virtual` for lazy loading support.
- Some properties are decorated with `[DefaultValue]` or `[NotMapped]` as needed.
- Relationships are configured via data annotations (`[ForeignKey]`, `[Key]`) for clarity and EF Core compatibility.

---

This structure allows seamless database interaction via EF Core, while maintaining a clean, domain-driven model architecture.
