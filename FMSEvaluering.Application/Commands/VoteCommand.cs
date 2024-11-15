using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Application.Commands
{
    public class VoteCommand : IVoteCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVoteRepository _voteRepository;
        private readonly IPostRepository _postRepository;

        public VoteCommand(IUnitOfWork unitOfWork, IVoteRepository voteRepository, IPostRepository postRepository)
        {
            _unitOfWork = unitOfWork;
            _voteRepository = voteRepository;
            _postRepository = postRepository;
        }

        void IVoteCommand.CreateVote(CreateVoteDto voteDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                // Load
                var post = _postRepository.GetPost(voteDto.PostId);

                // do
                var vote = Vote.Create(voteDto.VoteType, post);

                // Save
                _voteRepository.AddVote(vote);

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
