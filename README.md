# PsychoShare Network API

> A social network platform for psychologists to share Creative Commons licensed bibliographic materials

## 🎯 Project Description

PsychoShare Network is a specialized social platform designed for psychology professionals to share, discover, and interact with Creative Commons licensed academic content. The platform provides standard social media functionality tailored for academic collaboration.

## 🛠️ Technical Stack

- **Backend**: .NET 8.0 with C#
- **Database**: MySQL 8.0+ with Entity Framework Core
- **Architecture**: Clean Architecture (DAO Pattern)
- **Authentication**: JWT (JSON Web Tokens)
- **ORM**: Entity Framework Core with Lazy Loading
- **Testing**: xUnit (In Progress)

## ✨ Core Features

### User Management
- User registration and authentication
- Profile management with image upload
- Account deletion functionality

### Content Sharing
- PDF upload and sharing
- Creative Commons licensing compliance
- File management system

### Social Features
- User following system
- Like and comment functionality
- Content discovery
- User interaction management

## 🏗️ Architecture

This project follows Clean Architecture principles with clear separation of concerns:

- **Controllers**: Handle HTTP requests and responses
- **Services**: Business logic implementation
- **Repositories**: Data access layer
- **Models**: Data entities and DTOs

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- MySQL 8.0+
- WAMP/XAMPP (for local MySQL)

### Database Setup
1. Create a MySQL database named `psychoshare`
2. Configure environment variables in `.env.local`:
```
DB_SERVER=localhost
DB_PORT=3306
DB_NAME=psychoshare
DB_USER=root
DB_PASSWORD=your_password
```

### Installation
```bash
# Clone the repository
git clone https://github.com/flavianoelia/PsychoShare-network-api.git

# Navigate to project directory
cd PsychoShare-network-api

# Restore dependencies
dotnet restore

# Run migrations (if needed)
dotnet ef database update

# Run the application
cd psychoshare_api
dotnet run
```

The API will be available at `http://localhost:5174`

## 🌿 Development Guidelines

### Git Workflow
- Work on feature branches
- Main branches: `main`, `development2`
- Write clear commit messages in English
- Test before pushing

### Code Standards
- Follow Clean Code principles
- Use descriptive variable names
- All code and comments in English
- Document complex logic

## 🧪 Testing

Unit tests are in development. Run tests with:

```bash
dotnet test
```

## 📚 API Documentation

Once the server is running, access Swagger documentation at:
```
http://localhost:5174/swagger
```

## 📁 Related Repositories

- **Frontend**: `psychoshare-public-site` (HTML Vanilla, CSS3, JS + Bootstrap)
- **Admin Dashboard**: `psychoshare-admin-dashboard` (React + Node.js)

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

**Note**: While the project code is MIT licensed, the platform is designed for users to share **Creative Commons licensed bibliographic content**. The Creative Commons licensing applies to the academic materials shared by users, not to the application code itself.

## 👥 Contributing

1. Create a feature branch with descriptive kebab-case name
2. Implement feature with unit tests
3. Ensure all code and comments are in English
4. Submit pull request with clear description 
