# Development Context for PsychoShare Network API

## Quick Reference

### Project Type
Social network for psychologists sharing Creative Commons academic content

### Key Business Rules
- Only Creative Commons licensed content allowed
- Psychology professionals are target users
- Academic/professional focus over casual social media

### Architecture Decisions
- Clean Architecture implementation
- Repository pattern for data access
- Service layer for business logic
- DTOs for data transfer
- Unit of Work pattern (likely)

### Database Schema Considerations
- Users (psychologists profiles)
- Posts (bibliographic materials)
- Files (PDF uploads)
- Likes, Comments, Follows (social features)
- Creative Commons metadata

### API Endpoints Structure (Proposed)
```
/api/auth          - Authentication
/api/users         - User management
/api/posts         - Content sharing
/api/files         - File upload/download
/api/social        - Likes, comments, follows
/api/admin         - Admin operations
```

### Development Priorities
1. User authentication system
2. File upload with CC licensing
3. Basic social features (follow, like, comment)
4. User profile management
5. Content discovery
6. Admin functionality

### Testing Strategy
- Unit tests for all business logic
- Integration tests for API endpoints
- File upload testing
- Authentication flow testing

### Security Considerations
- File upload validation (PDF only)
- User authentication/authorization
- Creative Commons compliance validation
- Data protection for user profiles

### Performance Considerations
- File storage optimization
- Database indexing for social queries
- Caching strategy for frequently accessed content
