using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Infrastructure.ExternalServices.Dto;

namespace FMSEvaluering.Infrastructure.ExternalServices
{
    public interface IFmsProxy
    {
        Task<string> StudentIsPartOfClassroom(string studentId);
    }
}
