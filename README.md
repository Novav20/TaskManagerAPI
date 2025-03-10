# TaskManager API

TaskManager is a **FastEndpoints**-based REST API for managing tasks efficiently. It supports **JWT authentication**, **PostgreSQL database integration**, and follows best practices for modern .NET applications.

---

## 🚀 Features
- ✅ **FastEndpoints** for high-performance API development
- 🔒 **JWT Authentication** using `BCrypt` for password hashing
- 📦 **Entity Framework Core** with PostgreSQL integration
- 📄 **CRUD operations** for managing tasks
- 📜 **Swagger UI** for API documentation

---

## 📂 Project Structure
```
TaskManager/
├── TaskManager/            # Main API project
│   ├── Endpoints/         # FastEndpoints for CRUD operations
│   ├── Models/            # Data models
│   ├── Services/          # Business logic services
│   ├── Data/              # Database context
│   ├── appsettings.json   # Configuration file
│   ├── Program.cs         # Entry point
├── README.md              # Project documentation
```

---

## 🛠️ Setup & Installation

### 1️⃣ Clone the Repository
```sh
git clone https://github.com/Novav20/TaskManagerAPI.git
cd TaskManagerAPI
```

### 2️⃣ Configure Database
Update **`appsettings.json`** with your PostgreSQL connection string:
```json
"ConnectionStrings": {
  "PostgresConnection": "Host=localhost;Database=taskdb;Username=postgres;Password=yourpassword"
}
```

### 3️⃣ Run Migrations & Update Database
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4️⃣ Run the Application
```sh
dotnet run
```
The API will be available at **`http://localhost:5000`** (or `https://localhost:5001`).

---

## 🔑 Authentication (JWT)

### Register a User
```http
POST /auth/register
```
```json
{
  "username": "user1",
  "password": "securepassword"
}
```

### Get Token
```http
POST /auth/login
```
```json
{
  "username": "user1",
  "password": "securepassword"
}
```
Response:
```json
{
  "token": "your.jwt.token"
}
```
Use this token in the `Authorization` header for secured endpoints:
```
Authorization: Bearer your.jwt.token
```

---

## 📌 API Endpoints
| Method | Endpoint | Description | Auth Required |
|--------|---------|-------------|--------------|
| `POST` | `/auth/register` | Register a new user | ❌ |
| `POST` | `/auth/login` | Authenticate & get JWT | ❌ |
| `POST` | `/task` | Create a new task | ✅ |
| `GET` | `/tasks` | Retrieve all tasks | ✅ |
| `GET` | `/tasks/{id}` | Get task by ID | ✅ |
| `PUT` | `/tasks/{id}` | Update a task | ✅ |
| `DELETE` | `/tasks/{id}` | Delete a task | ✅ |

---

## 📚 Technologies Used
- **.NET 9 & C# 13**
- **FastEndpoints**
- **PostgreSQL & EF Core**
- **JWT Authentication**
- **Swagger for API Documentation**

---

## 🛠️ Development
To contribute, create a new feature branch:
```sh
git checkout -b feature-branch
```
After making changes:
```sh
git add .
git commit -m "Added new feature"
git push origin feature-branch
```

---

## 📜 License
This project is **MIT Licensed**. Feel free to use and modify it.

---

## ✉️ Contact
For any issues or suggestions, open an [issue](https://github.com/Novav20/TaskManagerAPI/issues) or reach out!

