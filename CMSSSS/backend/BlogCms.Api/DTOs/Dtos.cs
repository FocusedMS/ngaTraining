namespace BlogCms.Api.DTOs;

// Auth
public record RegisterRequest(string Email, string Username, string Password);
public record LoginRequest(string UsernameOrEmail, string Password);

public class AuthResponse
{
    public string Token { get; set; } = "";
    public int ExpiresIn { get; set; } = 3600;
    public object User { get; set; } = new();
}

// Posts
public class PostCreateRequest
{
    public string Title { get; set; } = "";
    public string? Excerpt { get; set; }
    public string ContentHtml { get; set; } = "";
    public int? CategoryId { get; set; }
    public List<int>? TagIds { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? FocusKeyword { get; set; }
}
public class PostUpdateRequest : PostCreateRequest {}

public class PostResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Slug { get; set; } = "";
    public string? Excerpt { get; set; }
    public string ContentHtml { get; set; } = "";
    public string? CoverImageUrl { get; set; }
    public string Status { get; set; } = "";
    public int AuthorId { get; set; }
    public int? CategoryId { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

// Moderation
public record ModerationDecisionRequest(string? Reason);

// Pagination
public class PagedResult<T>
{
    public required IEnumerable<T> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}
