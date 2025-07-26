# PsychoShare Network API

> A social network platform for psychologists to share Creative Commons licensed bibliographic materials

## 🎯 Project Description

PsychoShare Network is a specialized social platform designed for psychology professionals to share, discover, and interact with Creative Commons licensed academic content. The platform provides standard social media functionality tailored for academic collaboration.

## 🛠️ Technical Stack

- **Backend**: .NET 8.0 with C#
- **Database**: MySQL/SQL Server
- **Architecture**: Clean Architecture
- **Authentication**: C# Authentication System
- **Deployment**: Docker
- **Testing**: Unit Testing Required

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
- MySQL/SQL Server
- Docker (for deployment)

### Installation
```bash
# Clone the repository
git clone https://github.com/flavianoelia/PsychoShare-network-api.git

# Navigate to project directory
cd PsychoShare-network-api

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

## 🌿 Development Guidelines

### Git Workflow
- Create feature branches using kebab-case naming
- Example: `user-authentication`, `file-upload-feature`
- Write brief, explicit commit messages
- All commits and code must be in English

### Code Standards
- Follow Clean Code principles
- Use descriptive variable names
- Include unit tests for all features
- Avoid accessibility-unfriendly symbols (@, x, /)

### Branch Naming Convention
❌ **Incorrect**: `feature/john-user-login`
✅ **Correct**: `feature/user-authentication`

## 🧪 Testing

Unit tests are required for all implemented features. Run tests with:

```bash
dotnet test
```

## 📁 Related Repositories

- **Frontend**: `psychoshare-public-site` (HTML Vanilla, CSS3, JS + Bootstrap)
- **Admin Dashboard**: Coming soon (React + Node.js)

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

**Note**: While the project code is MIT licensed, the platform is designed for users to share **Creative Commons licensed bibliographic content**. The Creative Commons licensing applies to the academic materials shared by users, not to the application code itself.

## 👥 Contributing

1. Create a feature branch with descriptive kebab-case name
2. Implement feature with unit tests
3. Ensure all code and comments are in English
4. Submit pull request with clear description 
