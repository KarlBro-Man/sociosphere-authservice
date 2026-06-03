namespace AuthService.Features.Auth.Models;

public class RefreshToken
{
    public long Id {get; set;}
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? RevokedAt { get; set; }
    public bool IsRevoked => RevokedAt != null;
    public long UserId { get; set; }
    public User User { get; set; } = null!;
}