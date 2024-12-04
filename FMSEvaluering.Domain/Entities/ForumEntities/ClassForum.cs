using FMSEvaluering.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

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

    public override async Task<bool> ValidateUserAccessAsync(string appUserId, IServiceProvider serviceProvider,
        string role)
    {
        switch (role)
        {
            case "student":
                var studentDomainService = serviceProvider.GetRequiredService<IStudentDomainService>();
                var studentDto = await studentDomainService.GetStudentAsync(appUserId);
                return ValidateStudentAccessAsync(studentDto);
            case "teacher":
                var teacherDomainService = serviceProvider.GetRequiredService<ITeacherDomainService>();
                var teacherDto = await teacherDomainService.GetTeacherAsync(appUserId);
                return ValidateTeacherAccessAsync(teacherDto);
        }
        return false;
    }

    public override bool ValidateStudentAccessAsync(StudentDto studentDto)
    {
        return ClassId.ToString().Equals(studentDto.Class.Id);
    }
    public override bool ValidateTeacherAccessAsync(TeacherDto teacherDto)
    {
        return teacherDto.TeacherSubjects.Any(ts => ts.Class.Id.Equals(ClassId.ToString()));
    }
}