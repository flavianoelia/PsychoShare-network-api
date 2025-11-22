# Development Guide - PsychoShare Network API

## 📖 Project Overview

**PsychoShare Network** is a specialized social platform for psychology professionals to share Creative Commons licensed academic content, scientific articles, and educational materials.

## 🏗️ Current Architecture

### Technology Stack
- **.NET 8.0** with C#
- **MySQL 8.0+** with Entity Framework Core
- **JWT Authentication** for secure API access
- **DAO Pattern** for data access layer
- **Clean Architecture** principles

### Project Structure
```
PsychoShare-network-api/
├── psychoshare_api/          # Main API project
│   ├── Controllers/          # API endpoints (14 controllers)
│   ├── DTOs/                 # Data Transfer Objects
│   ├── Services/             # Business logic
│   ├── Validations/          # Input validation
│   └── Configurations/       # App configuration
├── entity_library/           # Domain entities
│   ├── system/              # User, Role, Ban
│   ├── social_media_core/   # Post, Comment, Like
│   ├── following/           # Following system
│   ├── media/               # Image, Pdf, Avatar
│   └── ReportPolicy/        # Report system
└── dao_library/             # Data access layer
    ├── interfaces/          # DAO interfaces
    ├── entity_framework/    # EF implementations
    ├── Mock/                # Mock implementations
    └── Migrations/          # Database migrations
```

### Related Projects
- **Frontend**: `psychoshare-public-site` (HTML5, CSS3, Bootstrap, Vanilla JS)
- **Admin Dashboard**: `psychoshare-dashboard-admin` (React - In Planning)

## ✅ Implemented Features

### Authentication & Authorization
- ✅ JWT token generation and validation
- ✅ User registration and login
- ✅ Role-based access control
- ✅ Token refresh mechanism

### User Management
- ✅ User CRUD operations
- ✅ Profile management
- ✅ Avatar upload and management
- ✅ Email verification check
- ✅ Password hashing (BCrypt)

### Social Features
- ✅ **Posts**: Full CRUD with pagination and search
- ✅ **Comments**: Create, read, update, delete
- ✅ **Likes**: Like/unlike posts
- ✅ **Following**: Follow/unfollow users, get followers/following lists
- ✅ Feed system with pagination (5 posts per page)

### Media Management
- ✅ Image upload and storage
- ✅ PDF upload and management
- ✅ File type validation
- ✅ Avatar management

### Admin Features
- ✅ **Reports**: Report system for content moderation
- ✅ **Bans**: User banning system
- ✅ **Roles**: Role assignment and management

## 🚀 API Endpoints (Implemented)

### Authentication (`/api/auth`)
```
POST   /api/auth/login          - User login (returns JWT)
POST   /api/auth/register       - User registration
POST   /api/auth/refresh        - Refresh JWT token
```

### Users (`/api/user`)
```
POST   /api/user                - Register new user
GET    /api/user/{id}           - Get user by ID
PUT    /api/user/edit/{id}      - Update user profile
GET    /api/user/check-email    - Check if email exists
```

### Posts (`/api/post`)
```
POST   /api/post                - Create new post
GET    /api/post/{id}           - Get post by ID
GET    /api/post                - Get all posts (paginated)
GET    /api/post/feed           - Get feed with search (paginated)
GET    /api/post/user/{userId}  - Get user's posts
PUT    /api/post/{id}           - Update post
DELETE /api/post/{id}           - Delete post
```

### Comments (`/api/comment`)
```
POST   /api/comment             - Create comment
GET    /api/comment/post/{id}   - Get post comments
PUT    /api/comment/{id}        - Update comment
DELETE /api/comment/{id}        - Delete comment
```

### Likes (`/api/like`)
```
POST   /api/like                - Like a post
DELETE /api/like/{id}           - Unlike a post
GET    /api/like/post/{id}      - Get post likes count
```

### Following (`/api/following`)
```
POST   /api/following/follow    - Follow a user
DELETE /api/following/unfollow  - Unfollow a user
GET    /api/following/followers/{userId}    - Get user followers
GET    /api/following/following/{userId}    - Get users being followed
GET    /api/following/count/followers/{id}  - Count followers
GET    /api/following/count/following/{id}  - Count following
```

### Media (`/api/image`, `/api/pdf`, `/api/avatar`)
```
POST   /api/image               - Upload image
POST   /api/pdf                 - Upload PDF
POST   /api/avatar              - Upload avatar
GET    /api/image/{id}          - Get image
GET    /api/pdf/{id}            - Get PDF
```

### Admin (`/api/report`, `/api/ban`, `/api/role`)
```
POST   /api/report              - Create report
GET    /api/report              - Get all reports
PUT    /api/report/{id}         - Update report status
POST   /api/ban                 - Ban user
GET    /api/ban/{userId}        - Check if user is banned
POST   /api/role/assign         - Assign role to user
```

## 🔄 Database Schema

### Core Tables
- **Users** - User accounts with authentication
- **Roles** - User roles (Admin, Moderator, User)
- **Person** - Extended user profile information
- **Posts** - Academic content and articles
- **Comments** - Post comments
- **Likes** - Post likes
- **Following** - User following relationships
- **Images** - Image files metadata
- **Pdfs** - PDF files metadata
- **Avatars** - User avatar images
- **Reports** - Content moderation reports
- **Bans** - Banned users records
- **ReportPolicy** - Report policy definitions

### Entity Relationships
- User → Person (1:1)
- User → Posts (1:N)
- User → Comments (1:N)
- User → Likes (1:N)
- User → Following (M:N)
- Post → Comments (1:N)
- Post → Likes (1:N)
- Post → Image (1:1)
- Post → Pdf (1:1)

## 🛠️ Development Setup

### Environment Variables (.env.local)
```bash
DB_SERVER=localhost
DB_PORT=3306
DB_NAME=psychoshare
DB_USER=root
DB_PASSWORD=your_password
JWT_SECRET=your_jwt_secret_key
```

### Running the Project
```bash
# Restore dependencies
dotnet restore

# Apply migrations
dotnet ef database update

# Run API
cd psychoshare_api
dotnet run

# API available at: http://localhost:5174
# Swagger docs: http://localhost:5174/swagger
```

## 📋 Current Priorities

### In Progress
- [ ] Unit testing implementation
- [ ] Profile edit functionality enhancement
- [ ] Search and filtering improvements
- [ ] File upload size limits and validation

### Planned
- [ ] Email verification system
- [ ] Password reset functionality
- [ ] Creative Commons license validation
- [ ] Content recommendation algorithm
- [ ] Admin dashboard (React app)
- [ ] Real-time notifications
- [ ] Advanced search filters

## 🧪 Testing

### Testing Strategy
- Unit tests for business logic (In Progress)
- Integration tests for API endpoints
- Manual testing via Swagger UI
- Postman collection for API testing

### Running Tests
```bash
dotnet test
```

## 📝 Code Standards

### Naming Conventions
- **Controllers**: `{Feature}Controller.cs`
- **DTOs**: `{Feature}{Purpose}Dto.cs`
- **Entities**: PascalCase
- **Methods**: PascalCase (C# convention)
- **Variables**: camelCase

### Git Workflow
- **Main branch**: `main` (production)
- **Development branch**: `development2`
- **Feature branches**: Create from `development2`
- **Commit messages**: Clear, descriptive, in English

### Code Review Checklist
- [ ] Code compiles without errors/warnings
- [ ] Follows C# naming conventions
- [ ] DTOs properly defined
- [ ] Validations implemented
- [ ] Error handling in place
- [ ] Comments only where necessary (code should be self-documenting)

## 🔐 Security Considerations

### Implemented
- ✅ JWT authentication
- ✅ Password hashing (BCrypt)
- ✅ Input validation
- ✅ File type validation
- ✅ Role-based authorization

### To Implement
- [ ] Rate limiting
- [ ] CORS configuration
- [ ] SQL injection prevention (using EF parameterized queries)
- [ ] XSS protection
- [ ] File upload size limits
- [ ] Content Security Policy

## 📚 Key Resources

- **Swagger UI**: `http://localhost:5174/swagger`
- **Database**: MySQL Workbench / phpMyAdmin
- **Frontend Repo**: [psychoshare-public-site](https://github.com/flavianoelia/psychoshare-public-site)
- **API Repo**: [PsychoShare-network-api](https://github.com/flavianoelia/PsychoShare-network-api)

## 🤝 Contributing

### For New Developers
1. Clone the repository
2. Set up environment variables (`.env.local`)
3. Run migrations: `dotnet ef database update`
4. Review this document and the codebase
5. Pick a task from the priorities list
6. Create a feature branch
7. Implement with proper validations and error handling
8. Test thoroughly via Swagger
9. Submit pull request to `development2`

### Need Help?
- Check Swagger documentation for API specs
- Review existing controllers for patterns
- Consult with team lead for architecture decisions
