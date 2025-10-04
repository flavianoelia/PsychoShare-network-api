# Database Configuration Setup# Database Configuration Setup



## For Local Development## For Local Development



1. Copy `.env.local.example` to `.env.local` (or create `.env.local` directly)1. Copy `.env.local.example` to `.env.local`

2. Update the database configuration in `.env.local` with your local settings:2. Update the database configuration in `.env.local` with your local settings:



```bash```bash

DB_SERVER=localhostDB_SERVER=localhost

DB_PORT=3306  DB_PORT=3306  

DB_NAME=your_database_name  # e.g., psychoshare, psychosharedb, etc.DB_NAME=psychoshare  # or your database name

DB_USER=root                # or your mysql userDB_USER=root         # or your mysql user

DB_PASSWORD=your_password   # your mysql password (empty for XAMPP default)DB_PASSWORD=         # your mysql password (empty for XAMPP default)

``````



## Database Names Used by Team:## Database Names Used by Team:

- **Flavia**: `psychoshare` (XAMPP, password: 1234)- **Flavia**: `psychoshare` (XAMPP, no password)

- **Professor**: `psychosharedb` (MySQL with password)- **Professor**: `psychosharedb` (MySQL with password)

- **Coty**: `psychoshare_coty` (custom setup)- **Add your config here...**

- **Add your config here...**

## Applying Migrations:

## Setting Up Your Database

```bash

### First Time Setup:dotnet ef database update --project dao_library --startup-project psychoshare_api

1. Install Entity Framework CLI tool:```

```bash

dotnet tool install --global dotnet-ef## Common Issues:

```- **Access denied**: Check your DB_USER and DB_PASSWORD in .env.local

- **Database not found**: Verify DB_NAME matches your local database

2. Create your `.env.local` file with your database settings- **Connection refused**: Make sure MySQL is running (XAMPP/MySQL Workbench)
3. Apply all migrations to create database structure:
```bash
dotnet ef database update --project dao_library --startup-project psychoshare_api
```

### When New Migrations Are Added:
```bash
dotnet ef database update --project dao_library --startup-project psychoshare_api
```

## Common Issues:

### Access denied error:
- Check your DB_USER and DB_PASSWORD in .env.local
- Verify MySQL is running (XAMPP/MySQL Workbench)

### Database not found:
- Verify DB_NAME matches your local database
- Create the database manually in phpMyAdmin/MySQL Workbench if needed

### Connection refused:
- Make sure MySQL service is running
- Check if port 3306 is correct for your setup

### dotnet-ef command not found:
```bash
dotnet tool install --global dotnet-ef
```

## Important Notes:
- **NEVER** commit your `.env.local` file (it's in .gitignore)
- **NEVER** modify `appsettings.Development.json` for database config
- Each team member should have their own database configuration
- Use generic database name `psychoshare_dev` if unsure

## Team Workflow:
1. Each developer maintains their own `.env.local`
2. Shared configuration stays in `appsettings.Development.json` (generic)
3. No more database configuration conflicts during git operations
4. New team members can quickly set up their environment