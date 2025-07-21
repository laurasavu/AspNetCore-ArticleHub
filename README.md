# 📰 ArticleHub
Project Overview
🎯 Project Goal
I developed a centralized web platform for managing scientific articles and enabling collaboration among researchers.

👥 Target Audience
The application is intended for research institutions, academic organizations, and companies in the scientific sector that require a robust technological solution for collaborative scientific publication management.

🛠️ Problem Addressed / Usefulness
Many research institutions face:

Fragmentation of scientific information across multiple platforms

Lack of a centralized overview of scientific output

Difficulty in collaboration between researchers from different departments

Main Features
✅ Full CRUD System (Create, Read, Update, Delete):

Article listing

Article creation

Article editing

Article deletion

Application Architecture and Technologies Used
Frontend (Client-side):
HTML5 / CSS3: Page structure and visual design

JavaScript ES6: Application logic on the browser level

Fetch API: Asynchronous communication with the server

Backend (Server-side):
ASP.NET Core 6.0: Main framework used for web application development

Entity Framework Core: ORM for accessing the PostgreSQL database

AutoMapper: Automatic mapping between domain entities and DTOs

Basic Authentication: Custom middleware for authentication and security

PostgreSQL: Relational database used for persistent data storage

Conclusion
The application demonstrates the implementation of a modern full-stack architecture, with a clear separation between frontend and backend, using current technologies and web development best practices.

Benefits
Centralization of scientific content on a single platform

Efficient collaboration between researchers across various fields

Tracking of project progress and deadlines

Version control and document revision management

Fast search across the article and study database

## 📸 Screenshots

### Homepage
https://github.com/laurasavu/AspNetCore-ArticleHub/blob/master/Screenshot%202025-06-11%20091930.png

### Article Search and List
https://github.com/laurasavu/AspNetCore-ArticleHub/blob/master/Screenshot%202025-06-11%20092057.png
### Add Article
https://github.com/laurasavu/AspNetCore-ArticleHub/blob/master/Screenshot%202025-06-11%20092004.png
### Edit Article
https://github.com/laurasavu/AspNetCore-ArticleHub/blob/master/Screenshot%202025-06-11%20092121.png

---

## ⚙️ Getting Started

### Prerequisites

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Setup Instructions

1. **Clone the repository**:
   ```bash
   git clone https://github.com/laurasavu/AspNetCore-ArticleHub.git
   cd AspNetCore-ArticleHub
2.   Update appsettings.json with your PostgreSQL connection string:
         "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=ArticleHubDB;Username=your_user;Password=your_password"
      }

3.dotnet ef database update

4.dotnet run

Feel free to fork the repo, create a feature branch, and submit a pull request. Suggestions and feedback are always welcome!
