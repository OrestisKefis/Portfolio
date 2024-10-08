using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Database.Requests.Admin;
using Portfolio.Models;
using Portfolio.Models.Interfaces;

namespace Portfolio.Admin.Projects;

public sealed class UpdateProject : IEndpoint
{
    internal static async Task<Results<Ok<Project>, ProblemHttpResult, NotFound>> Update(int id, IValidator<UpdateProjectRequest> validator, UpdateProjectRequest request, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return TypedResults.Problem(validationResult.ToString(), statusCode: 400);
        }

        var project = await context.Projects
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (project == null)
            return TypedResults.NotFound();

        project.Name = request.Name;
        project.DateCreated = request.DateCreated;
        project.LanguagesUsed = request.Languages;
        project.ImageUrl = request.ImageUrl;
        project.Description = request.Description;

        context.Projects.Update(project);
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
        app.MapPut("api/admin/projects/{id}", Update)
            .RequireAuthorization("IsAdmin");
    }
}

public sealed class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
        RuleFor(r => r.Description).NotEmpty();
        RuleFor(r => r.DateCreated).NotEmpty();
        RuleFor(r => r.Languages).NotEmpty();
        RuleFor(r => r.GithubPath).NotEmpty();
        RuleFor(r => r.ImageUrl).NotEmpty();
    }
}
