using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Domain.Entities.ForumEntities
{
    public class PublicForum : Forum
    {
        public PublicForum(string name)
        {
            Name = name;
        }

        public override bool ValidateStudentAccessAsync(StudentValue student)
        {
            return true;
        }
        public override bool ValidateTeacherAccessAsync(TeacherValue teacher)
        {
            return true;
        }
    }
}
