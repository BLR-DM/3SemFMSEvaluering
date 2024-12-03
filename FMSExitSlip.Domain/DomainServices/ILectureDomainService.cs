using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Domain.DomainServices
{
    public interface ILectureDomainService
    {
        Task<ValidateLectureDto> ValidateIfTeacherIsAPartOfLecture(string lectureId);
        Task<List<string>> GetStudentsForLectureAsync(string lectureId); //gets ids of students in a lecture
    }

    public record ValidateLectureDto(string LectureId, string TeacherId);
}
