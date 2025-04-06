# 🌿 Environmental Municipality Management System  

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-6.0%2B-blueviolet)
![SQL Server](https://img.shields.io/badge/SQL_Server-2019+-informational)
![License](https://img.shields.io/badge/License-MIT-green)

A modern web platform for citizens and municipal staff to manage environmental services, track pollution incidents, and streamline public health workflows.  

---

## ✨ Features  

| Feature | Description |  
|---------|-------------|  
| **📝 Service Requests** | Citizens submit waste collection/pollution requests with real-time tracking. |  
| **⚠️ Incident Reporting** |Descriptions of environmental hazards and report things. |  
| **👩‍💼 Staff Dashboard** | Create and manage Staff. |  
| **👩‍💼 Citizen Dashboard** | Generate Create and manage Citizens. |  

---

## 🛠️ Tech Stack  

- **Backend**: ASP.NET Core 9.0 (MVC)  
- **Frontend**: Razor Pages, Bootstrap 5  
- **Database**: SQL Server + Entity Framework Core  
- **Auth**: ASP.NET Core Identity  
- **Hosting**: Local Host http

---

## 🚀 Getting Started  

### Prerequisites  
- .NET 9.0 SDK  
- SQL Server 2019+  
- Visual Studio 2022 (or VS Code)  

### Installation  
1. Clone the repo:  
   ```bash
   https://github.com/JustorBot/Municipality.git
2. Configure the database:
Update appsettings.json with your SQL Server connection string.
Run migrations:
  ```bash
  dotnet ef database update
  ```
3. Run the app:
   ```bash
   dotnet run
   ```

---

## 📂 Project Structure

EnvironmentalMunicipality/

├── Controllers/

├── Models/

├── Views/

├── Data/

├── wwwroot/

└── appsettings.json

---

