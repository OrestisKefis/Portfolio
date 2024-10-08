using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Portfolio.Admin.Projects;
using Portfolio.Policies;
using Portfolio.Policies.Handlers;
using Portfolio.Database;
using Portfolio.Database.Requests.Admin;
using Portfolio.EndpointMappings;
using Portfolio.Models.Enums;

var builder = WebApplication.CreateBuilder(args);

//Adds dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
    }
});

//Adds authorization policies
builder.Services.AddSingleton<IAuthorizationHandler, AdminRoleHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", policy =>
    {
        policy.Requirements.Add(new AdminRoleRequirement(Role.Administrator));
    });
});

//Adds validators
builder.Services.AddScoped<IValidator<AddProjectRequest>, AddProjectValidator>();
builder.Services.AddScoped<IValidator<UpdateProjectRequest>, UpdateProjectValidator>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

//Adds reverse proxy
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
}

var app = builder.Build();

//Maps endpoints
UserMappings.MapEndpoints(app);
ProjectMappings.MapEndpoints(app);
ContactMeMappings.MapEndpoints(app);


//Adds reverse proxy
if (app.Environment.IsDevelopment())
{
    app.MapReverseProxy();
}

//app.MapFallbackToFile();

app.Run();