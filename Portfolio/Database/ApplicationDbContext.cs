using Microsoft.EntityFrameworkCore;
using Portfolio.Database.Entities;
using Portfolio.Models;

namespace Portfolio.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
        
    }

    public DbSet<ProjectEntity> Projects { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<EmailVerification> EmailVerifications { get; set; }
}
