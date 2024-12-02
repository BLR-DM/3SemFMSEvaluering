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
        public string TeacherId { get; set; }
    }
}
