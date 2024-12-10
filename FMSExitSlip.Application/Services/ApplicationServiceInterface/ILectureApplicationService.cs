using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Application.Services.ApplicationServiceInterface
{
    public interface ILectureApplicationService
    {
        Task<IEnumerable<LectureValue>> GetLecturesAsync();
    }
}
