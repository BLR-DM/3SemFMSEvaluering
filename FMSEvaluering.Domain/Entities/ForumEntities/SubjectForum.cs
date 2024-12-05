using FMSEvaluering.Domain.Values.DataServer;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Domain.Entities.ForumEntities
{
    public class SubjectForum : Forum
    {
        public int SubjectId { get; set; }

        internal SubjectForum(string name, int subjectId)
        {
            Name = name;
            SubjectId = subjectId;
        }

        public override bool ValidateStudentAccessAsync(StudentValue student)
        { 
            return student.Class.TeacherSubjects.Any(ts => ts.Id.Equals(SubjectId.ToString()));
        }
        public override bool ValidateTeacherAccessAsync(TeacherValue teacher)
        {
            return teacher.TeacherSubjects.Any(ts => ts.Id.Equals(SubjectId.ToString()));
        }
    }
}
