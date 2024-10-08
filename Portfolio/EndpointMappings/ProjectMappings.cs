using Portfolio.Admin.Projects;
using Portfolio.Models.Interfaces;
using Portfolio.Projects;

namespace Portfolio.EndpointMappings
{
    public class ProjectMappings : IMapping
    {
        public static void MapEndpoints(WebApplication app)
        {
            GetProjects.MapEndpoint(app);
            GetProject.MapEndpoint(app);
            GetFilteredProjects.MapEndpoint(app);
            AddProject.MapEndpoint(app);
            DeleteProject.MapEndpoint(app);
            UpdateProject.MapEndpoint(app);
        }
    }
}
