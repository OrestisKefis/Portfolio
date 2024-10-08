using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Models.Interfaces;

namespace Portfolio.Projects;

public sealed class GetProject : IEndpoint
{

    internal async static Task<Results<Ok<Project>, NotFound>> GetById(int id, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var project = await context.Projects
            .AsNoTracking()
            .Where(p => p.Id == id)
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
            .FirstOrDefaultAsync(cancellationToken);

        if (project is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(project);
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapGet("api/projects/{id}", GetProject.GetById);
    }
}
