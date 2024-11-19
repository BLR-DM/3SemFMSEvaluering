using FMSDataServer.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FMSDataServer.Api
{
    public class FMSDataDbContext : IdentityDbContext<AppUser>
    {

        public FMSDataDbContext(DbContextOptions<FMSDataDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<ModelClass> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    }
}
