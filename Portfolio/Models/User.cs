using Portfolio.Models.Enums;

namespace Portfolio.Models;

public class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public byte[] Salt { get; set; } = null!;

    public Role Role { get; set; }
}