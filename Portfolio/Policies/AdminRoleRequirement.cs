using Microsoft.AspNetCore.Authorization;
using Portfolio.Models.Enums;

namespace Portfolio.Policies
{
    public class AdminRoleRequirement(Role role) : IAuthorizationRequirement
    {
        public Role Role { get; } = role;
    }
}