using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Domain.Entities.PostEntities;
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
                a.Property(h => h.Description).IsRequired();
                a.Property(h => h.EditedDate).IsRequired();
                a.HasKey("PostId", "EditedDate");
            });

        modelBuilder.Entity<Forum>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<SubjectForum>("SubjectForum")
            .HasValue<ClassForum>("ClassForum")
            .HasValue<PublicForum>("PublicForum");
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Vote> Votes { get; set; } // Test
    public DbSet<Comment> Comments { get; set; } // Test
    public DbSet<Forum> Forums { get; set; }
}