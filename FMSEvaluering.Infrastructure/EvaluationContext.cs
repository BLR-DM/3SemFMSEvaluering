using FMSEvaluering.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure;

public class EvaluationContext : DbContext
{
    public EvaluationContext(DbContextOptions<EvaluationContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Vote> Votes { get; set; }
}