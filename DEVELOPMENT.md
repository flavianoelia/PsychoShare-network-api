# Development Context for PsychoShare Network API

## Quick Reference

### Project Type

Social network for psychology professionals to share Creative Commons licensed academic content and scientific divulgation

### Frontend Status (EXISTING)

- **Repository**: `psychoshare-public-site`
- **Technology Stack**: HTML5 Vanilla, CSS3, JavaScript ES6
- **Framework**: Bootstrap (included via external file)
- **Pages Completed**:
  - Login page
  - Sign-up page
  - Profile page
  - Wall (main feed)
  - Contacts page
  - About-us page
- **Features**: Responsive design, modals, post system UI, comments interface

### Key Business Rules

- Only Creative Commons licensed content allowed
- Psychology professionals are target users
- Academic/professional focus over casual social media
- Scientific divulgation and educational material sharing

### Architecture Decisions

- Clean Architecture implementation
- Repository pattern for data access
- Service layer for business logic
- DTOs for data transfer
- Unit of Work pattern (likely)

### Database Schema Considerations

- **Users** (psychologists profiles - connects to profile page)
- **Posts** (scientific articles and educational content - wall display)
- **Files** (PDF uploads with Creative Commons licensing)
- **Comments** (post interactions - wall comments system)
- **Likes** (post engagement - wall likes feature)
- **Follows/Contacts** (social connections - contacts page)
- **Creative Commons metadata** (licensing information)

### API Endpoints Structure (To match existing frontend)

```
/api/auth          - Authentication (login, sign-up pages)
/api/users         - User management (profile page)
/api/posts         - Content sharing (wall/feed functionality)
/api/files         - PDF upload/download (articles and materials)
/api/social        - Likes, comments, follows (wall interactions)
/api/contacts      - Contact management (contacts page)
/api/admin         - Admin operations
```

### Frontend-Backend Integration Points

1. **Login page** → `/api/auth/login`
2. **Sign-up page** → `/api/auth/register`
3. **Profile page** → `/api/users/{id}`, `/api/users/update`
4. **Wall page** → `/api/posts`, `/api/social/comments`, `/api/social/likes`
5. **Contacts page** → `/api/contacts`, `/api/social/follow`
6. **About-us page** → Static content (no API needed)

### Development Priorities (Based on existing frontend)

1. **User authentication system** (login/sign-up integration)
2. **User profile management** (profile page support)
3. **Posts and content sharing** (wall functionality)
4. **Social interactions** (comments, likes for wall)
5. **File upload with CC licensing** (PDF articles)
6. **Contact/follow system** (contacts page support)
7. **Admin functionality** (content moderation)

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

## 🌿 Git Workflow & Branch Management

### Branch Strategy
```
main (production-ready)
├── development (integration branch)
├── development2 (alternative integration)
└── feature/* (feature branches)
```

### Branch Targeting Rules
- **Feature branches** → `development` or `development2`
- **Hotfixes** → `main` (emergency only)
- **Documentation** → `development`

### Common Issues & Solutions

#### ❌ PR Targeting `main` Instead of `development`
**Problem**: PR was created targeting `main` branch
**Solution**: 
1. Change target branch in GitHub UI, OR
2. Close PR and recreate targeting `development`

#### ❌ Duplicate PRs
**Problem**: Multiple PRs with same changes
**Solution**: 
1. Review both PRs
2. Close duplicate with reference: "Duplicate of #[number]"
3. Keep the PR with better description/targeting

#### ❌ Wrong Branch Name
**Problem**: Branch doesn't follow conventions
**Solution**: 
1. Create new branch with correct name
2. Cherry-pick commits: `git cherry-pick <commit-hash>`
3. Delete old branch

### Quick Commands
```bash
# Check current branch and status
git status

# List all branches
git branch -a

# Change PR target (GitHub UI only)
# Go to PR → Edit → Change base branch

# Delete problematic branch
git push origin --delete branch-name
git branch -D branch-name
```
