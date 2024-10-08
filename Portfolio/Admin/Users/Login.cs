using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Admin.Users;

internal sealed class Login : IEndpoint
{
    public static async Task<Results<Ok, ProblemHttpResult>> Handle(LoginRequest request, ApplicationDbContext context, HttpContext httpContext, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == request.Email);

        if (user is null)
        {
            return TypedResults.Problem($"The user with username {request.Email} does not exist");
        }

        bool isPasswordVerified = VerifyPasswordHash(request.Password, user.Salt, user.Password);

        if (!isPasswordVerified)
        {
            return TypedResults.Problem("Password was wrong");
        }

        List<Claim> claims =
        [
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, user.Role.ToString())
        ];

        var properties = new AuthenticationProperties
        {
            AllowRefresh = false,
            IsPersistent = false,
            ExpiresUtc = DateTime.UtcNow.AddDays(1),
        };

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, CreateClaimsIdentity(claims), properties);

        return TypedResults.Ok();
    }

    public static ClaimsPrincipal CreateClaimsIdentity(IEnumerable<Claim> claims)
    {
        return new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
    }

    private static bool VerifyPasswordHash(string password, byte[] storedSalt, byte[] storedHash)
    {
        using var hmac = new HMACSHA256(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return storedHash.SequenceEqual(computedHash);
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapPost("/admin/login", Handle);
    }
}
