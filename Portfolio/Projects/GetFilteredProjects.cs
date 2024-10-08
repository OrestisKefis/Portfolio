using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Models.Enums;
using Portfolio.Models.Interfaces;

namespace Portfolio.Projects;

internal sealed class GetFilteredProjects : IEndpoint
{
    private static async Task<Ok<List<Project>>> Get(GetFilteredProjectsRequest request, ApplicationDbContext context, CancellationToken cancellationToken)
      {
        var query = context.Projects
            .AsNoTracking();

        if (request.SearchValue.Trim() != String.Empty)
        {
            query = query.Where(p => p.Name.Contains(request.SearchValue));
        }

        if (request.Languages.Length > 0)
        {
            query = query.Where(p => request.Languages.All(l => p.LanguagesUsed!.Contains(l)));
        }

        var projects = await query.Select(project => new Project
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
        app.MapPost("api/projects/filter", Get);
    }
}

public sealed record GetFilteredProjectsRequest(string SearchValue, Language[] Languages);