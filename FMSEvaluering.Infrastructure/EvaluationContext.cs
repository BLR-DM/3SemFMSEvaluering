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
        modelBuilder.Entity<Post>().OwnsMany(
            p => p.History, a =>
            {
                a.WithOwner().HasForeignKey("PostId");
                a.Property(h => h.Content).IsRequired();
                a.Property(h => h.EditedDate).IsRequired();
                a.HasKey("PostId", "EditedDate");
            });
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Vote> Votes { get; set; } // Test
    public DbSet<Comment> Comments { get; set; } // Test
}