using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Domain.DomainServices
{
    public interface ITeacherAuthorizationDomainService
    {
        Task<ValidateLectureDto> ValidateIfTeacherIsAPartOfLecture(string lectureId);
    }

    public record ValidateLectureDto(string LectureId, string TeacherId);
}
