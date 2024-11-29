using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.ExternalServices;
using FMSEvaluering.Domain.DomainService;

namespace FMSEvaluering.Application.Service
{
    public class ClassroomAccessService : IClassroomAccessService
    {
        private readonly IFmsDataProxy _fmsDataProxy;

        public ClassroomAccessService(IFmsDataProxy fmsDataProxy)
        {
            _fmsDataProxy = fmsDataProxy;
        }

        async Task<string> IClassroomAccessService.GetStudentClassId(string studentId)
        {
            
            var classId = await _fmsDataProxy.GetStudentClassId(studentId);
            return classId;
        }
    }
}
