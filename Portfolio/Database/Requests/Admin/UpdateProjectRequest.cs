using Portfolio.Models.Enums;

namespace Portfolio.Database.Requests.Admin;

public sealed record UpdateProjectRequest(string Name, DateOnly DateCreated, Language[] Languages, string GithubPath, string ImageUrl, string Description);