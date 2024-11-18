using FMSEvaluering.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure;

public class EvaluationContext : DbContext
{
    public EvaluationContext(DbContextOptions<EvaluationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.OwnsMany(p => p.History, history =>
            {
                //history.WithOwner().HasForeignKey("PostId"); // Optional FK customization
                history.Property(h => h.Content).IsRequired(); // Configure the property
            });
        });
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Vote> Votes { get; set; } // Test
    public DbSet<Comment> Comments { get; set; } // Test
}