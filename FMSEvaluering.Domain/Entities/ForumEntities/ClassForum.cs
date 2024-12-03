using FMSEvaluering.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Domain.Entities.ForumEntities
{
    public class ClassForum : Forum
    {
        protected ClassForum()
        {
        }

        public int ClassId { get; set; }

        public ClassForum(string name, int classId)
        {
            Name = name;
            ClassId = classId;
        }
        
        public override async Task<bool> ValidateUserAccessAsync(string appUserId, IServiceProvider serviceProvider, string role)
        {
            switch (role)
            {
                case "student":
                    var studentDomainService = serviceProvider.GetRequiredService<IStudentDomainService>();
                    var studentDto = await studentDomainService.GetStudentAsync(appUserId);
                    return ClassId.ToString().Equals(studentDto.Class.Id);

                case "teacher":
                    var teacherDomainService = serviceProvider.GetRequiredService<ITeacherDomainService>();
                    var teacherDto = await teacherDomainService.GetTeacherAsync(appUserId);
                    return teacherDto.TeacherSubjects.Any(ts => ts.Class.Id == ClassId.ToString());
            }
            return false;
        }
    }
}
