## BookStore API

ASP.NET Core Web API implementing CRUD for books and authors with RESTful attribute routing, using Entity Framework Core InMemory database.

### Run
- `dotnet run` in `BookStoreApi`
- Swagger UI: `http://localhost:5067/swagger`

### Models
- Book: `Id`, `Title` (required, max 200), `AuthorId` (required), `PublicationYear` (0-3000)
- Author: `Id`, `Name` (required, max 120)

### Routes
- Authors
  - GET `api/authors`
  - GET `api/authors/{id}`
  - POST `api/authors`
  - PUT `api/authors/{id}`
  - DELETE `api/authors/{id}`
- Books
  - GET `api/books`
  - GET `api/books/{id}`
  - GET `api/authors/{authorId}/books`
  - POST `api/books`
  - PUT `api/books/{id}`
  - DELETE `api/books/{id}`

### Examples (JSON)
Author POST body:
```json
{ "name": "J. K. Rowling" }
```

Book POST body:
```json
{ "title": "HP 1", "authorId": 1, "publicationYear": 1997 }
```

### Status Codes
- 200 OK, 201 Created, 204 No Content
- 400 Validation errors
- 404 Not Found (missing book/author)

### Test with Postman
1. Create author, note `id`.
2. Create book using `authorId`.
3. GET books and books by author.
4. Update and delete.
5. Try invalid bodies to see 400, unknown ids to see 404.

### Inspect with Fiddler
- Verify methods, URIs, headers (`Content-Type: application/json`).
- Check status codes and response bodies for each action.


