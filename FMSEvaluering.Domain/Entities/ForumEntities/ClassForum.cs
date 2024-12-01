using FMSEvaluering.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Domain.Entities.ForumEntities
{
    public class ClassForum : Forum
    {
        private readonly IServiceProvider _serviceProvider;
        public int ClassId { get; set; }

        internal ClassForum(string name, int classId, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Name = name;
            ClassId = classId;
        }
        
        public override async Task<bool> ValideUserAccessToForum(string appUserId)
        {
            var fmsDataService = _serviceProvider.GetRequiredService<IValidateStudentDomainService>();
            var fmsValidationResponse = await fmsDataService.ValidateUserAccess(appUserId);

            if (!ClassId.ToString().Equals(fmsValidationResponse.ClassId))
                throw new InvalidOperationException("Student is not part of class.");
            return true;
        }
    }
}
