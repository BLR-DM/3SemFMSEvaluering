using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.CommandDto.QuestionDto;
using FMSExitSlip.Application.Commands.CommandDto.ResponseDto;
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
        private readonly IExitSlipAccessHandler _exitSlipAccessHandler;

        public ExitSlipCommand(IUnitOfWork unitOfWork, IExitSlipRepository exitSlipRepository, IExitSlipAccessHandler exitSlipAccessHandler)
        {
            _unitOfWork = unitOfWork;
            _exitSlipRepository = exitSlipRepository;
            _exitSlipAccessHandler = exitSlipAccessHandler;
        }

        async Task IExitSlipCommand.AddQuestionAsync(CreateQuestionDto questionDto, int exitSlipId, string appUserId, string role)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Validate Access
                await _exitSlipAccessHandler.ValidateExitslipAccess(appUserId, role, exitSlip);

                // Do
                exitSlip.CreateQuestion(questionDto.Text, appUserId);
                _exitSlipRepository.AddQuestion(exitSlip);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IExitSlipCommand.CreateResponseAsync(CreateResponseDto responseDto, int exitSlipId, string appUserId, string role)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Validate Access
                await _exitSlipAccessHandler.ValidateExitslipAccess(appUserId, role, exitSlip);

                // Do
                exitSlip.CreateResponse(responseDto.Text, appUserId, responseDto.QuestionId);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IExitSlipCommand.UpdateResponseAsync(UpdateResponseDto responseDto, int exitSlipId, string appUserId, string role)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Validate Access
                await _exitSlipAccessHandler.ValidateExitslipAccess(appUserId, role, exitSlip);

                // Do
                var response = exitSlip.UpdateResponse(responseDto.ResponseId, responseDto.Text, appUserId, responseDto.QuestionId);
                _exitSlipRepository.UpdateResponse(response, responseDto.RowVersion);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IExitSlipCommand.DeleteResponseAsync(DeleteResponseDto responseDto, int exitSlipId, string appUserId, string role)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Validate Access
                await _exitSlipAccessHandler.ValidateExitslipAccess(appUserId, role, exitSlip);

                // Do
                var response = exitSlip.DeleteResponse(responseDto.ResponseId, appUserId, responseDto.QuestionId);
                _exitSlipRepository.DeleteResponse(response, responseDto.RowVersion);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IExitSlipCommand.DeleteQuestionAsync(DeleteQuestionDto questionDto, int exitSlipId, string appUserId, string role)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Validate Access
                await _exitSlipAccessHandler.ValidateExitslipAccess(appUserId, role, exitSlip);

                // Do
                var question = exitSlip.DeleteQuestion(questionDto.Id, appUserId);
                _exitSlipRepository.DeleteQuestion(question, questionDto.RowVersion);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IExitSlipCommand.CreateExitSlipAsync(CreateExitSlipDto exitSlipDto) // BLIVER IKKE BRUGT
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var otherExitSlips = await _exitSlipRepository.GetExitSlipsAsync();


                // Do
                var exitSlip = ExitSlip.Create(exitSlipDto.Title, exitSlipDto.MaxQuestions, exitSlipDto.LectureId, otherExitSlips);
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

        async Task IExitSlipCommand.PublishExitSlip(int exitSlipId, string appUserId, PublishExitSlipDto exitSlipDto, string role)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Validate Access
                await _exitSlipAccessHandler.ValidateExitslipAccess(appUserId, role, exitSlip);

                // Do
                exitSlip.Publish(appUserId);
                _exitSlipRepository.PublishExitSlip(exitSlip, exitSlipDto.RowVersion);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IExitSlipCommand.UpdateQuestionAsync(UpdateQuestionDto questionDto, int exitSlipId, string appUserId, string role)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Validate Access
                await _exitSlipAccessHandler.ValidateExitslipAccess(appUserId, role, exitSlip);

                // Do
                var response = exitSlip.UpdateQuestion(questionDto.Id, questionDto.Text, appUserId);
                _exitSlipRepository.UpdateQuestion(response, questionDto.RowVersion);

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
