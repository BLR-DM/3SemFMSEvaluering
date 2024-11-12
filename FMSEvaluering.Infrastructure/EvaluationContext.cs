using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMSEvaluering.Infrastructure
{
    public class EvaluationContext : DbContext
    {
        public EvaluationContext(DbContextOptions<EvaluationContext> options) : base(options)
        {
        }

        public DbSet<EvaluationPost> EvaluationPosts { get; set; }
    }
}
