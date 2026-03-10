# YourTodos – Task Management Web Application
## 📌 About the Project

YourTodos is a task management web application built with ASP.NET Core Razor Pages.
The application allows users to create, manage, and track their personal tasks with additional features like email reminders and authentication.

The project was developed as part of a graduate work to practice building a real-world web application using ASP.NET Core, Entity Framework Core, and ASP.NET Identity.

Users can create tasks, edit them, delete them, and schedule reminders that will be sent via email using SendGrid.

## 🚀 Features

### ✅ Task Management

- Create new tasks
- Edit existing tasks
- Delete tasks
- View all tasks in a list
- Search tasks

### ⏰ Email Reminders

- Schedule reminders for tasks
- Email notifications sent via SendGrid
- Background reminder service checks scheduled tasks

### 👤 User Authentication

- User registration
- User login/logout
- Account management
- Profile settings

## 🔐 Security

- ASP.NET Identity authentication
- User-specific task lists

## 📖 Guide Page

Built-in guide explaining how to use the application

## 🛠️ Tech Stack

### Backend / Web Framework

- ASP.NET Core
- Razor Pages
- Entity Framework Core
- Authentication
- ASP.NET Core Identity
- Microsoft Azure
- Email Service
- SendGrid
- Background Processing
- Hosted Background Service

## 📸 Screenshots

### DESKTOP

<img width="1920" height="1030" alt="Screenshot (1520)" src="https://github.com/user-attachments/assets/494d664f-3e67-4ed6-b82a-01a36c95720e" />
<img width="1920" height="1025" alt="Screenshot (1521)" src="https://github.com/user-attachments/assets/0b09df87-b762-4b82-8775-304478afa776" />
<img width="1920" height="1030" alt="Screenshot (1522)" src="https://github.com/user-attachments/assets/31b59f73-8a73-4b6d-bc05-19de70d2efec" />
<img width="1920" height="1030" alt="Screenshot (1523)" src="https://github.com/user-attachments/assets/72c0a549-9aa2-42c8-921b-47c9a7da3f4e" />
<img width="1920" height="1025" alt="Screenshot (1524)" src="https://github.com/user-attachments/assets/4cf03555-f70e-48fb-b3e3-0ed0bb2c06a1" />
<img width="1920" height="1030" alt="Screenshot (1525)" src="https://github.com/user-attachments/assets/5df445de-0db6-4f34-9d74-8f4b52473004" />
<img width="1920" height="1027" alt="Screenshot (1526)" src="https://github.com/user-attachments/assets/1d75579c-e6b7-4b4d-9ba5-9c3b1ed407a2" />
<img width="1920" height="1027" alt="Screenshot (1527)" src="https://github.com/user-attachments/assets/3f7fc971-d821-45c1-9218-920d7271a1b7" />

### MOBILE

<img width="627" height="968" alt="Screenshot (1528)" src="https://github.com/user-attachments/assets/36752e2f-0143-475c-8055-5cdc46d08bfc" />
<img width="626" height="966" alt="Screenshot (1529)" src="https://github.com/user-attachments/assets/1ab57de1-08f8-463d-8381-66db45c23f0a" />
<img width="625" height="966" alt="Screenshot (1531)" src="https://github.com/user-attachments/assets/a0df52b2-0795-4dff-8090-07fab8a8bd23" />
<img width="626" height="965" alt="Screenshot (1530)" src="https://github.com/user-attachments/assets/57acb242-4a5c-4ac2-b7e7-36da0e7cef4a" />


## ⚙️ Installation

Clone Repository
git clone https://github.com/20Amir04/ToDo-List-WebSite-.git
cd ToDo-List-WebSite-
Configure SendGrid

Open:

appsettings.json
Add your SendGrid API key:

"SendGrid": {
  "ApiKey": "YOUR_API_KEY"
}
Run the Application
dotnet restore
dotnet run

## 👨‍💻 Author

Amir Arabi

Software engineering Student
Full-Stack Developer (Intern/Junior)

GitHub:
https://github.com/20amir04
