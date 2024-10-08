using Portfolio.Contact;
using Portfolio.Models.Interfaces;

namespace Portfolio.EndpointMappings;

public class ContactMeMappings : IMapping
{
    public static void MapEndpoints(WebApplication app)
    {
        ContactMe.MapEndpoint(app);
    }
}
