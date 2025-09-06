# AI-Powered Resume Builder

Implements the capstone requirements: roles (Guest, RegisteredUser, Admin), JWT auth, resume CRUD, AI suggestions, secure PDF download, Swagger docs, and a premium blue/white React UI.

## Run (Backend)
```bash
cd backend/ResumeApi
dotnet restore
dotnet ef database update   # (after adding a migration)
dotnet run
```
> Configure `appsettings.json` `ConnectionStrings:DefaultConnection` for your SQL Server.
> Set JWT secrets in `appsettings.json` for production.
> Set `Cors:AllowedOrigins` to your frontend URLs.

## Run (Frontend)
```bash
cd frontend
npm i
npm run dev
```
Set `VITE_API_URL` in `.env.local` if your backend URL differs.

## What’s included
- ASP.NET Core 8 Web API (+ Identity + EF Core SQL Server)
- JWT + Role seeding (Admin, RegisteredUser, Guest)
- Resume CRUD with owner/Admin authorization
- AI suggestions endpoint (section-aware, persisted)
- PDF export (owner/Admin only)
- Swagger with Bearer JWT
- React + Vite + TypeScript UI (premium blue/white palette)
- Admin page: list users + metrics

## Docs
- See `docs/` for API overview and roles.
- Swagger (dev): `http://localhost:5189/swagger`.

## Next steps
- Add migrations (`dotnet ef migrations add Init`), then update DB.
- Plug in your SMTP (optional) and enhance templates.
- See Testing and Deployment guides in chat.