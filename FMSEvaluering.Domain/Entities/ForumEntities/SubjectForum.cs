using FMSEvaluering.Domain.DomainServices;
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

        public override async Task<bool> ValidateUserAccessAsync(string appUserId, IServiceProvider serviceProvider, string role)
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
            return studentDto.Class.TeacherSubjects.Any(ts => ts.Id.Equals(SubjectId.ToString()));
        }
        public override bool ValidateTeacherAccessAsync(TeacherDto teacherDto)
        {
            return teacherDto.TeacherSubjects.Any(ts => ts.Id.Equals(SubjectId.ToString()));
        }
    }
}
