using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Infrastructure.ExternalServices
{
    public interface IFmsDataProxy
    {
        Task<LectureResultDto> GetLectureAsync(string lectureId);
    }

    public record LectureResultDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public TeacherSubjectResultDto TeacherSubject { get; set; }
    }

    public record TeacherSubjectResultDto
    {
        public string Id { get; set; }
        public TeacherResultDto Teacher { get; set; }
        public ClassResultDto Class { get; set; }
    }

    public record ClassResultDto
    {
        public IEnumerable<StudentResultDto> Students { get; set; }
    }

    public record StudentResultDto
    {
        public string AppUserId { get; set; }
    }

    public record TeacherResultDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AppUserId { get; set; }
    }
}
