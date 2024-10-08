using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Models.Interfaces;

namespace Portfolio.Projects;

internal sealed class GetProjects : IEndpoint
{
    private async static Task<Ok<List<Project>>> Get(ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var projects = await context.Projects
            .AsNoTracking()
            .Select(project => new Project
            {
                Id = project.Id,
                Name = project.Name,
                DateCreated = project.DateCreated,
                Description = project.Description,
                GithubPath = project.GithubPath,
                ImageUrl = project.ImageUrl,
                LanguagesUsed = project.LanguagesUsed
            })
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(projects);
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapGet("api/projects", Get);
    }
}
