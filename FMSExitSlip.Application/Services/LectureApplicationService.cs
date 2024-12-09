using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Services.ApplicationServiceInterface;
using FMSExitSlip.Application.Services.ProxyInterface;
using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Services
{
    public class LectureApplicationService : ILectureApplicationService
    {
        private readonly IFmsDataProxy _fmsDataProxy;

        public LectureApplicationService(IFmsDataProxy fmsDataProxy)
        {
            _fmsDataProxy = fmsDataProxy;
        }
        async Task<IEnumerable<LectureValue>> ILectureApplicationService.GetLecturesAsync()
        {
            var lectureResultDto = await _fmsDataProxy.GetLecturesAsync();
            return lectureResultDto.Select(lecture => new LectureValue
            {
                Id = lecture.Id,
                Date = lecture.Date
            });
        }
    }
}
