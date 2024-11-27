using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.Interfaces;
using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Repositories;
using FMSExitSlip.Domain.Entities;


namespace FMSExitSlip.Application.Commands
{
    public class ExitSlipCommand : IExitSlipCommand
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IExitSlipRepository _exitSlipRepository;

        public ExitSlipCommand(IUnitOfWork unitOfWork, IExitSlipRepository exitSlipRepository)
        {
            _unitOfWork = unitOfWork;
            _exitSlipRepository = exitSlipRepository;
        }

        async Task IExitSlipCommand.CreateQuestion(CreateExitSlipDto exitSlipDto)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                //var exitSlip = _exitSlipRepository


            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task IExitSlipCommand.CreateExitSlipAsync(string title, int maxQuestions, string appUserId, int lectureId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var otherExitSlips = await _exitSlipRepository.GetExitSlipsAsync();

                // Do
                var exitSlip = ExitSlip.Create(title, maxQuestions, appUserId, lectureId, otherExitSlips);
                await _exitSlipRepository.AddExitSlipAsync(exitSlip);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
