using FMSDataServer.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FMSDataServer.Api
{
    public class FMSDataDbContext : DbContext
    {

        public FMSDataDbContext(DbContextOptions<FMSDataDbContext> options) : base(options)
        {
        }

        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<ModelClass> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    }
}
