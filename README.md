# 📰 ArticleHub

**ArticleHub** is a lightweight ASP.NET Core web application designed for managing peer-reviewed journal articles. 
Initially intended for scientific publications, it now features a collection of programming-related articles for demo.
It supports CRUD operations, basic authentication, and PostgreSQL as the backend database.

---

## 🚀 Features

- 🔍 Search for articles by keyword
- 🧑 Author management and article attribution
- ✍️ Create, edit, and delete articles (CRUD)
- 🖼️ Clean and responsive UI built with HTML/CSS
- 🧰 Built using ASP.NET Core and PostgreSQL

---

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core (.NET 6+)
- **Frontend**:  HTML, CSS, JavaScript
- **Database**: PostgreSQL


---

## 📸 Screenshots

### Homepage
![Homepage](https://github.com/laurasavu/AspNetCore-ArticleHub/Screenshot%202025-06-11%20091930.png)

### Article Search and List
![Article List](https://github.com/laurasavu/AspNetCore-ArticleHub/Screenshot%202025-06-11%20092057.png)

### Edit Article
![Edit Article](https://github.com/laurasavu/AspNetCore-ArticleHub/Screenshot%202025-06-11%20092121.png)

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

