using Microsoft.AspNetCore.Http.HttpResults;
using Portfolio.Database.Entities;
using Portfolio.Database;
using Portfolio.Models.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Portfolio.Database.Requests.Admin;
using Portfolio.Models;

namespace Portfolio.Admin.Projects;

internal sealed class AddProject : IEndpoint
{
    public async static Task<Results<Ok<Project>, ProblemHttpResult>> Add(AddProjectRequest request, IValidator<AddProjectRequest> validator, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return TypedResults.Problem(validationResult.ToString());
        }

        if (request.Languages.Length < 0)
        {
            return TypedResults.Problem("No language has been selected", statusCode: 400);
        }

        var project = new ProjectEntity
        {
            Name = request.Name,
            DateCreated = request.DateCreated,
            LanguagesUsed = request.Languages,
            GithubPath = request.GithubPath,
            ImageUrl = request.ImageUrl,
            Description = request.Description,
        };

        context.Add(project);
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
        app.MapPost("api/admin/projects", Add)
            .RequireAuthorization("IsAdmin");
    }
}

public sealed class AddProjectValidator : AbstractValidator<AddProjectRequest>
{
    public AddProjectValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
        RuleFor(r => r.Description).NotEmpty();
        RuleFor(r => r.DateCreated).NotEmpty();
        RuleFor(r => r.Languages).NotEmpty();
        RuleFor(r => r.GithubPath).NotEmpty();
        RuleFor(r => r.ImageUrl).NotEmpty();
    }
}