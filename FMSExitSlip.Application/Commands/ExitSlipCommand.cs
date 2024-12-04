using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IServiceProvider _serviceProvider;

        public ExitSlipCommand(IUnitOfWork unitOfWork, IExitSlipRepository exitSlipRepository, IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _exitSlipRepository = exitSlipRepository;
            _serviceProvider = serviceProvider;
        }

        async Task IExitSlipCommand.AddQuestionAsync(CreateQuestionDto questionDto, string appUserId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(questionDto.exitSlipId);

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

        async Task IExitSlipCommand.CreateResponseAsync(CreateResponseDto responseDto, int exitSlipId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Do
                exitSlip.CreateResponse(responseDto.Text, responseDto.AppUserId, responseDto.QuestionId);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IExitSlipCommand.UpdateResponseAsync(UpdateResponseDto responseDto, int exitSlipId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Do
                var response = exitSlip.UpdateResponse(responseDto.ResponseId, responseDto.Text, responseDto.AppUserId, responseDto.QuestionId);
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

        async Task IExitSlipCommand.DeleteResponseAsync(DeleteResponseDto responseDto, int exitSlipId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

                // Do
                var response = exitSlip.DeleteResponse(responseDto.ResponseId, responseDto.AppUserId, responseDto.QuestionId);
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

        async Task IExitSlipCommand.DeleteQuestionAsync(DeleteQuestionDto questionDto, int exitSlipId, string appUserId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

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

        async Task IExitSlipCommand.CreateExitSlipAsync(CreateExitSlipDto exitSlipDto, string appUserId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var otherExitSlips = await _exitSlipRepository.GetExitSlipsAsync();

                // Do
                var exitSlip = ExitSlip.Create(exitSlipDto.Title, exitSlipDto.MaxQuestions, exitSlipDto.LectureId, appUserId, otherExitSlips, _serviceProvider);
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

        async Task IExitSlipCommand.PublishExitSlip(int id, string appUserId, PublishExitSlipDto exitSlipDto)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(id);

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

        async Task IExitSlipCommand.UpdateQuestionAsync(UpdateQuestionDto questionDto, int exitSlipId, string appUserId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var exitSlip = await _exitSlipRepository.GetExitSlipAsync(exitSlipId);

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
