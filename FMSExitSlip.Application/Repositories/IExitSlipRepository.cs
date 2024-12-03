using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.Entities;

namespace FMSExitSlip.Application.Repositories
{
    public interface IExitSlipRepository
    {
        Task AddExitSlipAsync(ExitSlip exitSlip);
        Task<ExitSlip> GetExitSlipAsync(int id);
        Task<IEnumerable<ExitSlip>> GetExitSlipsAsync();
        void UpdateResponse(Response response, byte[] rowVersion);
        void DeleteResponse(Response response, byte[] rowVersion);
        void PublishExitSlip(ExitSlip exitSlip, byte[] rowVersion);
        void UpdateQuestion(Question question, byte[] rowVersion);
        void DeleteQuestion(Question question, byte[] rowVersion);
    }
}
