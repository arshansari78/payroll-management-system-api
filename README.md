Payroll Management System API

A backend application developed using ASP.NET Core Web API to manage employee information and streamline payroll operations. The system provides structured APIs for managing employees, departments, designations, salary structures, and payroll records.

🚀 Project Overview

The Payroll Management System is designed to simplify employee and salary management through organized RESTful APIs. It follows a layered architecture to maintain clean code, separation of concerns, and better maintainability.

🛠️ Technologies Used

- C#
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- RESTful APIs
- JWT Authentication

✨ Key Features

1. Employee Management

- Add new employees
- Update employee details
- Get employee information
- Search employees
- Activate and deactivate employees
- Check existing employee information

2. Department Management

- Create and update departments
- Get department details
- Check department name availability
- Activate and deactivate departments
- Get employee count by department

3. Designation Management

- Manage employee designations
- Add and update designation records
- Retrieve designation information

4. Salary Structure Management

- Create salary structures
- Update existing salary structures
- Manage employee salary details

5. Payroll Management

- Manage payroll records
- Update payroll information
- Organize employee payroll operations

🏗️ Project Architecture

The project follows a layered architecture:

PayrollManagementSystem/
│
├── Models/
│
├── ViewModels/
│
├── Repository/
│
├── Services/
│
├── Controllers/
│
└── Program.cs

Architecture Overview:

- Models: Defines the application's data entities.
- ViewModels: Handles data transfer between API layers.
- Repository: Manages database operations.
- Services: Contains business logic.
- Controllers: Handles HTTP requests and API responses.

⚙️ Installation and Setup

1. Clone Repository

git clone YOUR_GITHUB_REPOSITORY_URL

2. Open Project

Open the solution in Visual Studio.

3. Configure Database

Update the database connection string in "appsettings.json" according to your MySQL configuration.

4. Restore Dependencies

dotnet restore

5. Apply Database Migrations

dotnet ef database update

6. Run Application

dotnet run

🎯 Project Objective

The main objective of this project is to develop a structured and maintainable payroll management solution that simplifies employee data management, salary structure handling, and payroll operations through RESTful APIs.

👨‍💻 Developer

Mohammed Arsh Ansari

GitHub: "arshansari78" (https://github.com/arshansari78)

---

⭐ Thank you for visiting this repository!
