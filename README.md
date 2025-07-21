
**ArticleHub** is a centralized web application designed for managing scientific articles and enabling efficient collaboration between researchers. It is tailored for research institutions, academic organizations, and scientific companies that require a robust and scalable solution for managing publications and research documents.

---

## 🎯 Goals

- Provide a unified platform for managing scientific publications
- Enable collaboration across research teams and departments
- Simplify tracking, editing, and versioning of scientific work

---

## 🔍 Problem Addressed

Many research institutions struggle with:
- Fragmentation of scientific data across multiple systems
- Lack of centralized visibility over scientific output
- Difficulty in managing collaboration across departments

**ArticleHub** addresses these issues by offering a full-stack, maintainable, and extensible platform that supports modern collaboration workflows.

---

## ⚙️ Features

- ✅ Full **CRUD operations** (Create, Read, Update, Delete) for articles
- 📄 Article listing, creation, editing, and deletion
- 🔐 Basic Authentication for secured access
- 🔄 Version management support and modular design
- 🔍 Search and filter capabilities for scientific content

---

## 🛠️ Technologies Used

### Frontend
- **HTML5 / CSS3** – Responsive layout and visual design
- **JavaScript (ES6)** – Client-side logic and interactivity
- **Fetch API** – Asynchronous server communication

### Backend
- **ASP.NET Core 6.0** – Web framework
- **Entity Framework Core** – ORM for PostgreSQL
- **AutoMapper** – Object-to-object mapping between domain entities and DTOs
- **Custom Middleware** – Basic authentication for user access control
- **PostgreSQL** – Relational database for persistent data storage

---

## 🧱 Architecture

The app follows a **modern full-stack architecture** with clear separation of concerns:
- Decoupled frontend and backend components
- Scalable data layer using EF Core and PostgreSQL
- Maintainable codebase structured around clean architecture principles

---

## ✅ Benefits

- 🧠 Centralized management of scientific content
- 👥 Improved collaboration between multidisciplinary teams
- ⏱️ Easy tracking of publication deadlines and progress
- 🔄 Support for version control and revisions
- 🔎 Fast search across the article database


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
