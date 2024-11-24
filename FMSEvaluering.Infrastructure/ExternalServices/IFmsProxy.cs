using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Infrastructure.ExternalServices
{
    public interface IFmsProxy
    {
        Task<string> StudentIsPartOfClassroom(string studentId);
    }
}
