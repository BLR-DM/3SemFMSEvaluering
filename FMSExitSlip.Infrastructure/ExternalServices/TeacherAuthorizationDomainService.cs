using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.DomainServices;

namespace FMSExitSlip.Infrastructure.ExternalServices
{
    public class TeacherAuthorizationDomainService : ITeacherAuthorizationDomainService
    {
        private readonly IFmsDataProxy _proxy;

        public TeacherAuthorizationDomainService(IFmsDataProxy proxy)
        {
            _proxy = proxy;
        }
        async Task<ValidateLectureDto> ITeacherAuthorizationDomainService.ValidateIfTeacherIsAPartOfLecture(string lectureId)
        {
            var result = await _proxy.GetLectureAsync(lectureId);
            return new ValidateLectureDto(result.Id, result.TeacherSubject.TeacherId);
        }
    }
}
