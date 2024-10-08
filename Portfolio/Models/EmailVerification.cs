namespace Portfolio.Models;

public class EmailVerification
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime ExpirationDate { get; set; }
}
