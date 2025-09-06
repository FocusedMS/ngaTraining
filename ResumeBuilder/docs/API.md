# API Overview

Base URL: `http://localhost:5189`

## Auth
- POST `/api/auth/register` — Register a new user
- POST `/api/auth/login` — Login, returns `{ token, role, email, name }`

## Resumes
- GET `/api/resumes` — List my resumes (Admin may pass `?all=1`)
- GET `/api/resumes/{id}` — Get resume by id (owner/Admin)
- POST `/api/resumes` — Create resume
- PUT `/api/resumes/{id}` — Update resume (owner/Admin)
- DELETE `/api/resumes/{id}` — Delete resume (owner/Admin)
- GET `/api/resumes/download/{id}` — Download PDF (owner/Admin)
- POST `/api/resumes/{id}/ai-suggestions` — Generate + persist suggestions
- POST `/api/resumes/{id}/ai-suggestions/preview` — Preview suggestions (no save)

## Admin
- GET `/api/admin/users` — List users (Admin)
- GET `/api/admin/metrics` — Totals + last 24h/7d resume counts (Admin)

Swagger UI (Dev): `http://localhost:5189/swagger`

---

# Roles
- Guest: access public pages only
- RegisteredUser: can create, edit, download own resumes; get AI suggestions
- Admin: all RegisteredUser privileges + user list and metrics

# Auth
- Use JWT Bearer in `Authorization` header.
