using DnsClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Portfolio.Database.Requests.Contact;
using Portfolio.Models.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Portfolio.Contact;

public sealed class ContactMe : IEndpoint
{
    internal static async Task<Results<Ok, ProblemHttpResult>> Send(ContactMeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var email = "myappmyrules@outlook.com";

            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            //TODO: Should come from a config file
            client.Credentials = new NetworkCredential(email, "FromGreeceW!thL0ve_042024");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email);
            mailMessage.To.Add("raven1996@hotmail.gr");
            mailMessage.Subject = request.Subject;
            mailMessage.Body = request.Message;

            mailMessage.ReplyToList.Add(new MailAddress(request.Email));

            client.Send(mailMessage);
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            return TypedResults.Problem($"Email could not be sent: ${ex}", statusCode: 400);
        }
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapPost("api/contact-me", Send);
    }
}
