# Blog CMS — Fullstack (Frontend + Backend)

This ZIP contains a ready-to-run **.NET 8 Web API** backend and a **Vite + React + TypeScript** frontend.

## Requirements
- .NET SDK 8
- Node.js 18+ and npm
- SQL Server (your instance: `ANANDANITHA\SQLEXPRESS`)

## Backend — Setup
```bash
cd backend/BlogCms.Api
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
# Swagger at http://localhost:5000/swagger
```
If you already have tables from previous attempts, change DB name in `appsettings.Development.json` (e.g., `BlogCmsCapstone2`) and rerun the migration.

Default seeded admin: **admin / Admin@123**

## Frontend — Setup
```bash
cd frontend
npm i
# Ensure .env has VITE_API_BASE_URL=http://localhost:5000
npm run dev
```
Frontend at **http://localhost:5173**.

## Notes
- Media uploads go to `wwwroot/media/{folder}`.
- Use Swagger's **Authorize** button to paste JWT for protected endpoints.
- SEO analyzer, moderation queue, and author-only routes require auth.
