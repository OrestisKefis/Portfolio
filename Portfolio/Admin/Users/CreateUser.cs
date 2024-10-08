using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Database.Requests.Admin;
using Portfolio.Models.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Admin.Users;

internal sealed class CreateUser : IEndpoint
{
    public async static Task<Results<NoContent, ProblemHttpResult>> Create(CreateUserRequest request, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == request.Email);

        if (user is not null)
        {
            return TypedResults.Problem($"User with username {request.Email} already exists", statusCode: 400);
        }

        byte[] passwordHash = [];
        byte[] salt = [];

        CreatePasswordHash(request.Password, out salt, out passwordHash);

        user = new Models.User
        {
            UserName = request.Email,
            Salt = salt,
            Password = passwordHash,
            Role = request.Role
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }

    private static void CreatePasswordHash(string password, out byte[] salt, out byte[] passwordHash)
    {
        var hmac = new HMACSHA256();
        salt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapPost("api/admin/create-User", Create)
            .RequireAuthorization("IsAdmin");
    }
}