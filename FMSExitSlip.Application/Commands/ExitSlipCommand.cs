using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Commands.Interfaces;
using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Repositories;
using FMSExitSlip.Domain.Entities;

namespace FMSExitSlip.Application.Commands
{
    public class ExitSlipCommand : IExitSlipCommand
    {
        private readonly IExitSlipRepository _exitSlipRepository;
        private readonly IUnitOfWork _unitOfwork;

        public ExitSlipCommand(IExitSlipRepository exitSlipRepository, IUnitOfWork unitOfwork)
        {
            _exitSlipRepository = exitSlipRepository;
            _unitOfwork = unitOfwork;
        }
        async Task IExitSlipCommand.CreateExitSlipAsync(string title, int maxQuestions, string appUserId, int lectureId)
        {
            try
            { 
                await _unitOfwork.BeginTransaction();

                // Load
                var otherExitSlips = await _exitSlipRepository.GetExitSlipsAsync();

                // Do
                var exitSlip = ExitSlip.Create(title, maxQuestions, appUserId, lectureId, otherExitSlips);
                await _exitSlipRepository.AddExitSlipAsync(exitSlip);

                // Save
                await _unitOfwork.Commit();
            }
            catch (Exception)
            {
                await _unitOfwork.Rollback();
                throw;
            }
        }
    }
}
