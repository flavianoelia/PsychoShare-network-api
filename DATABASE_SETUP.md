# Database Configuration Setup

## For Local Development

1. Copy `.env.local.example` to `.env.local`
2. Update the database configuration in `.env.local` with your local settings:

```bash
DB_SERVER=localhost
DB_PORT=3306  
DB_NAME=psychoshare  # or your database name
DB_USER=root         # or your mysql user
DB_PASSWORD=         # your mysql password (empty for XAMPP default)
```

## Database Names Used by Team:
- **Flavia**: `psychoshare` (XAMPP, no password)
- **Professor**: `psychosharedb` (MySQL with password)
- **Add your config here...**

## Applying Migrations:

```bash
dotnet ef database update --project dao_library --startup-project psychoshare_api
```

## Common Issues:
- **Access denied**: Check your DB_USER and DB_PASSWORD in .env.local
- **Database not found**: Verify DB_NAME matches your local database
- **Connection refused**: Make sure MySQL is running (XAMPP/MySQL Workbench)