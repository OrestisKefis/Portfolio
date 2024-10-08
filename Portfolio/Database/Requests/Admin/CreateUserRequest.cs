using Portfolio.Models.Enums;

namespace Portfolio.Database.Requests.Admin;

public record CreateUserRequest(string Email, string Password, Role Role);