using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Database;
using Portfolio.Database.Requests.Contact;
using Portfolio.Models;
using Portfolio.Models.Interfaces;

namespace Portfolio.Contact;

public sealed class AddEmailVerification : IEndpoint
{
    internal async static Task<Results<Ok<EmailVerification>, ProblemHttpResult>> Add(AddEmailVerificationRequest request, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        if (request.Email.IsNullOrEmpty())
        {
            return TypedResults.Problem("The email is empty", statusCode: 400);
        }

        var verification = new EmailVerification { Email = request.Email };
        verification.Token = GenerateToken();
        verification.ExpirationDate = DateTime.Now.AddMinutes(10);

        context.Add(verification);
        await context.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok(verification);
    }

    private static string GenerateToken()
    {
        var options = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        string token = string.Empty;
        Random rand = new Random();
        for (int i = 0; i < options.Length; i++)
        {
            token += rand.Next(0, options.Length);
        } 
        return token;
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapPost("api/contact-me/verifications/add", Add);
    }
}