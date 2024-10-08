using Portfolio.Models.Enums;

namespace Portfolio.Database.Requests.Admin;

public sealed record UpdateUserRequest(Role UserRole);
