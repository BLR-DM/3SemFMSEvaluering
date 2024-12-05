using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Domain.Entities.ForumEntities;

public class ClassForum : Forum
{
    protected ClassForum()
    {
    }

    public ClassForum(string name, int classId)
    {
        Name = name;
        ClassId = classId;
    }

    public int ClassId { get; set; }

    public override bool ValidateStudentAccessAsync(StudentValue student)
    {
        return ClassId.ToString().Equals(student.Class.Id);
    }
    public override bool ValidateTeacherAccessAsync(TeacherValue teacher)
    {
        return teacher.TeacherSubjects.Any(ts => ts.Class.Id.Equals(ClassId.ToString()));
    }
}