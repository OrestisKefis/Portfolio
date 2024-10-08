using Microsoft.AspNetCore.Http.HttpResults;
using Portfolio.Database;
using Portfolio.Models.Interfaces;

namespace Portfolio.Admin.Users;

internal sealed class DeleteUser : IEndpoint
{
    public static async Task<Results<Ok, ProblemHttpResult>> Delete(int id, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var user = context.Users.FirstOrDefault(u => u.Id == id);

        if (user is null)
        {
            return TypedResults.Problem($"User with id {id} does not exist!", statusCode: 400);
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapDelete("api/admin/users/{id}", Delete)
            .RequireAuthorization("IsAdmin");
    }
}
