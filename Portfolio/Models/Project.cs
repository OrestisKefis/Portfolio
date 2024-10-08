using Portfolio.Models.Enums;

namespace Portfolio.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateOnly? DateCreated { get; set; }

    public Language[]? LanguagesUsed { get; set; } = [];

    public string? GithubPath { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }
}
