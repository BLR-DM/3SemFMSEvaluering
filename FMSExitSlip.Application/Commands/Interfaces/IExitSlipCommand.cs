using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.Entities;

namespace FMSExitSlip.Application.Commands.Interfaces
{
    public interface IExitSlipCommand
    {
        Task CreateExitSlipAsync(string title, int maxQuestions, string appUserId, int lectureId);
    }
}
