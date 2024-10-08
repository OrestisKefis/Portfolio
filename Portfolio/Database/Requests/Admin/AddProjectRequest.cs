using Portfolio.Models.Enums;

namespace Portfolio.Database.Requests.Admin;

public sealed record AddProjectRequest(string Name, DateOnly DateCreated, Language[] Languages, string GithubPath, string ImageUrl, string Description);