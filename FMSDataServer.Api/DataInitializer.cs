using Microsoft.EntityFrameworkCore;

namespace FMSDataServer.Api
{
    public class DataInitializer
    {
        private readonly FMSDataDbContext _context;

        public DataInitializer(FMSDataDbContext context)
        {
            _context = context;
        }

        public async Task InsertData()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            var sql = 
                @"INSERT INTO Classes (Name) VALUES ('DMVE231');
                INSERT INTO Classes (Name) VALUES ('DMVE232'); 
                INSERT INTO Teachers (FirstName, LastName, Email) VALUES ('Lucas', 'MacQuarrie', 'lucas123@gmail.com');
                INSERT INTO Teachers (FirstName, LastName, Email) VALUES ('Ida', 'Holstborg-Pedersen', 'ida@gmail.com');
                INSERT INTO Students (FirstName, LastName, Email, ClassId) VALUES ('Rasmus', 'Skov', 'skovbov@gmail.com', 1);
                INSERT INTO Students (FirstName, LastName, Email, ClassId) VALUES ('Kristian', 'Dahl', 'kristian@yahoo.com', 1);
                INSERT INTO Students (FirstName, LastName, Email, ClassId) VALUES ('Bilal', 'Kinali', 'BilalKinali@gmail.com', 2);
                INSERT INTO Subjects (name) VALUES ('Prog');
                INSERT INTO Subjects (name) VALUES ('Sym');
                INSERT INTO TeacherSubjects (ClassId, SubjectId, TeacherId) Values (1, 1, 1);
                INSERT INTO TeacherSubjects (ClassId, SubjectId, TeacherId) Values (2, 2, 1);
                INSERT INTO TeacherSubjects (ClassId, SubjectId, TeacherId) Values (1, 2, 2);
                INSERT INTO TeacherSubjects (ClassId, SubjectId, TeacherId) Values (2, 1, 2);
                INSERT INTO Lectures (Title, Date, TeacherSubjectId) Values ('Første gang med programmering i DMVE231', '08-11-2024', 1);
                INSERT INTO Lectures (Title, Date, TeacherSubjectId) Values ('Første gang med programmering i DMVE232', '08-11-2024', 4);";

            await _context.Database.ExecuteSqlRawAsync(sql);
            await transaction.CommitAsync();
        }
    }
}
