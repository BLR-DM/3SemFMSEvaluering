using FMSEvaluering.Application.Commands.CommandDto.EvaluationPostDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Application.Commands
{
    public class EvaluationPostCommand : IEvaluationPostCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEvaluationPostRepository _evaluationPostRepository;

        public EvaluationPostCommand(IUnitOfWork unitOfWork, IEvaluationPostRepository evaluationPostRepository)
        {
            _unitOfWork = unitOfWork;
            _evaluationPostRepository = evaluationPostRepository;
        }

        async Task IEvaluationPostCommand.CreateEvaluationPost(CreateEvaluationPostDto dto)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                // Do
                var evaluationPost = EvaluationPost.Create(dto.desctription);

                // Save
                await _evaluationPostRepository.AddEvaluationPost(evaluationPost);

                // Commit
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
