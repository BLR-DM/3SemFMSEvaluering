using FMSExitSlip.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMSExitSlip.Infrastructure
{
    public class ExitSlipContext : DbContext
    {
        public ExitSlipContext(DbContextOptions<ExitSlipContext> options) : base(options)
        {
            
        }

        public DbSet<ExitSlip> ExitSlips { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
    }
}
