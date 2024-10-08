using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Models.Interfaces;

namespace Portfolio.Admin.Projects;

internal sealed class DeleteProject : IEndpoint
{

    private async static Task<Results<Ok<Project>, ProblemHttpResult>> Delete(int id, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var project = await context.Projects
            .AsNoTracking()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (project is null)
        {
            return TypedResults.Problem($"Project with id {id} does not exist", statusCode: 400);
        }

        context.Remove(project);
        await context.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok(new Project
        {
            Id = project.Id,
            Name = project.Name,
            DateCreated = project.DateCreated,
            Description = project.Description,
            GithubPath = project.GithubPath,
            ImageUrl = project.ImageUrl,
            LanguagesUsed = project.LanguagesUsed
        });
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapDelete("api/admin/projects/{id}", Delete)
            .RequireAuthorization("IsAdmin");
    }
}