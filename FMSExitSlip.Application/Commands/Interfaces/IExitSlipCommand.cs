using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Commands.CommandDto.QuestionDto;
using FMSExitSlip.Domain.Entities;

namespace FMSExitSlip.Application.Commands.Interfaces
{
    public interface IExitSlipCommand
    {
        Task CreateQuestion(CreateQuestionDto questionDto, string appUserId);
        Task CreateExitSlipAsync(string title, int maxQuestions, string appUserId, int lectureId);
    }
}
