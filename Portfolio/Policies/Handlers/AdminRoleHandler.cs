using Microsoft.AspNetCore.Authorization;
using Portfolio.Models.Enums;
using System.Security.Claims;

namespace Portfolio.Policies.Handlers
{
    public class AdminRoleHandler : AuthorizationHandler<AdminRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRoleRequirement requirement)
        {
            var roleClaim = context.User.FindFirst(u => u.Type == ClaimTypes.Role);

            if (roleClaim is null)
            {
                return Task.CompletedTask;
            }

            Enum.TryParse<Role>(roleClaim.Value, false, out Role currentRole);

            if (currentRole == Role.Administrator)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}