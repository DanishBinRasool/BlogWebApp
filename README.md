# BlogApp

A modern Blog Web Application built with **ASP.NET Core MVC (.NET 10)** that allows users to create, manage, and interact with blog posts through a secure authentication and authorization system.

---

## Features

### Authentication & Authorization
- User Registration and Login
- Secure Authentication
- Role-Based Authorization
- Protected Routes

### Categories
- Create Categories
- Edit Categories
- Delete Categories
- View All Categories

### Posts
- Create Blog Posts (Authenticated Users Only)
- Edit Posts
- Delete Posts
- View All Posts
- View Individual Post Details
- Posts Organized by Categories

### Comments
- Add Comments (Authenticated Users Only)
- View Comments
- Delete Comments (Authorized Users)

### Rich Text Editor
- Create beautifully formatted blog posts
- Support for headings, bold, italic, lists, links, and other rich text formatting
- Enhanced writing experience for content creators

### Image Uploads
- Upload featured images for blog posts
- Display images alongside posts
- Secure image storage and management

---

## Access Control

The application uses **Role-Based Authorization**.

### Guest Users
- View Categories
- Browse Blog Posts
- Read Comments

### Authenticated Users
- Create Blog Posts
- Add Comments

### Administrators
- Manage Categories
- Manage Posts
- Manage Comments
- Access Administrative Features

---

## Technologies Used

- ASP.NET Core MVC (.NET 10)
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Razor Views
- Bootstrap 5
- LINQ
- Rich Text Editor
- Image Upload Support

---

## Project Structure

```text
BlogApp
│
├── Controllers
├── Models
├── ViewModels
├── Views
├── Data
├── Services
├── Repositories
├── wwwroot
├── Migrations
└── Program.cs
```

---

## Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server
- Visual Studio 2022 or later

### Installation

1. Clone the repository

```bash
git clone https://github.com/YourUsername/BlogApp.git
```

2. Navigate to the project

```bash
cd BlogApp
```

3. Update the connection string in `appsettings.json`.

4. Apply database migrations

```bash
dotnet ef database update
```

5. Run the application

```bash
dotnet run
```

---

## Application Workflow

1. Visitors can browse blog posts and categories without logging in.
2. Users must register and log in to create blog posts.
3. Only authenticated users can add comments.
4. Administrators manage categories, posts, and comments.
5. Authors can create rich, formatted blog posts using the integrated rich text editor.
6. Blog posts can include uploaded images to enhance content.

---

## Future Improvements

- Search Functionality
- Tags
- Likes and Reactions
- User Profiles
- Email Notifications
- Pagination
- Soft Delete
- REST API
- Unit Testing
- Dark Mode

---



Example sections:

- Home Page
- Categories
- Blog Details
- Rich Text Editor
- User Login
- Admin Dashboard

---

## License

This project is intended for learning and portfolio purposes.

---

## Author

**Danish Rasool**

- GitHub: https://github.com/DanishBinRasool
- LinkedIn: https://www.linkedin.com/in/danish-rasool/



