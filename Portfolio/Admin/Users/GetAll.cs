using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Models.Interfaces;

namespace Portfolio.Admin.Users;

internal sealed class GetAll : IEndpoint
{
    private async static Task<Ok<List<User>>> Get(ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var users = await context.Users
            .AsNoTracking()
            .Select(x => new User
            {
                Id = x.Id,
                UserName = x.UserName,
                Role = x.Role
            })
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(users);
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapGet("api/admin/users", Get)
            .RequireAuthorization("IsAdmin");
    }
}
