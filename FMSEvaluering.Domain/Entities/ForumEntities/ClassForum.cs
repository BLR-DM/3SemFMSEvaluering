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
        
        public override async Task<bool> ValidateUserAccessToForum(string appUserId, IServiceProvider serviceProvider, string role)
        {
            switch (role)
            {
                case "student":
                    var studentDomainService = serviceProvider.GetRequiredService<IStudentDomainService>();
                    var studentDto = await studentDomainService.GetStudentAsync(appUserId);
                    return ClassId.ToString().Equals(studentDto.ClassId);

                case "teacher":
                    var teacherDomainService = serviceProvider.GetRequiredService<IValidateTeacherDomainService>();
                    break;
            }
            var sd = serviceProvider.GetRequiredService<IStudentDomainService>();
            var fmsValidasdationResponse = await fmsDataService.ValidateUserAccess(appUserId, role);

            
        }
    }
}
