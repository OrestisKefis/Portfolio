using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Database.Requests.Admin;
using Portfolio.Models.Interfaces;

namespace Portfolio.Admin.Users;

public sealed class UpdateUser : IEndpoint
{
    internal async static Task<Results<Ok, ProblemHttpResult>> Edit(int id, UpdateUserRequest request, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user is null)
        {
            return TypedResults.Problem($"The user with id: {id} does not exist", statusCode: 400);
        }

        if (user.Role == request.UserRole)
        {
            return TypedResults.Problem($"The user is already assigned to this role: {request.UserRole}", statusCode: 400);
        }

        user.Role = request.UserRole;
        await context.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapPut("api/admin/users/{id}", Edit)
            .RequireAuthorization("IsAdmin");
    }
}
