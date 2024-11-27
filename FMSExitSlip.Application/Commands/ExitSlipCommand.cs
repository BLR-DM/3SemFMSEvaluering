using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.Interfaces;
using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Repositories;

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
        Task IExitSlipCommand.CreateQuestion(CreateExitSlipDto exitSlipDto)
        {
			try
            {
                _unitOfWork.BeginTransaction();

                // Load
                //var exitSlip = _exitSlipRepository


            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
