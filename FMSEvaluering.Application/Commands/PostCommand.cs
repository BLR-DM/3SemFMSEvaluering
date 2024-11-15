using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;

namespace FMSEvaluering.Application.Commands
{
    public class PostCommand : IPostCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;

        public PostCommand(IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
        }

        async Task IPostCommand.CreatePost(CreatePostDto dto)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                // Do
                var post = Post.Create(dto.Description);

                // Save
                await _postRepository.AddPost(post);

                // Commit
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IPostCommand.CreateVote(CreateVoteDto dto)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                // Load 
                var post = await _postRepository.GetPost(dto.PostId);

                // Do
                post.CreateVote(dto.VoteType);

                // Save
                _postRepository.AddVote();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IPostCommand.UpdateVote(UpdateVoteDto dto)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                // Load
                var post = await _postRepository.GetPost(dto.PostId);

                // Do
                post.UpdateVote(dto.Id, dto.VoteType);

                // Save
                await _postRepository.UpdateVote();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IPostCommand.DeleteVote(DeleteVoteDto dto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                // Load 
                var post = await _postRepository.GetPost(dto.PostId);
                // Do
                post.DeleteVote(dto.Id);
                // Save
                _postRepository.DeleteVote();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
