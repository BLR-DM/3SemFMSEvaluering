using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.DomainServices;

namespace FMSExitSlip.Infrastructure.ExternalServices
{
    public class LectureDomainService : ILectureDomainService
    {
        private readonly IFmsDataProxy _proxy;

        public LectureDomainService(IFmsDataProxy proxy)
        {
            _proxy = proxy;
        }

        async Task<List<string>> ILectureDomainService.GetStudentsForLectureAsync(string lectureId)
        {
            var result = await _proxy.GetLectureAsync(lectureId);
            return result.TeacherSubject.Class.Students.Select(s => s.AppUserId).ToList();
        }

        async Task<ValidateLectureDto> ILectureDomainService.ValidateIfTeacherIsAPartOfLecture(string lectureId)
        {
            var result = await _proxy.GetLectureAsync(lectureId);
            return new ValidateLectureDto(result.Id, result.TeacherSubject.Teacher.AppUserId);
        }
    }
}
