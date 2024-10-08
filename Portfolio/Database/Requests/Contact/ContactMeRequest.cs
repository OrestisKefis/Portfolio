namespace Portfolio.Database.Requests.Contact;

public sealed record ContactMeRequest(string Email, string FirstName, string LastName, string Subject, string Message);