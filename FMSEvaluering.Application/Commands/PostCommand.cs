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
using FMSEvaluering.Application.Commands.CommandDto.CommentDto;

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

        async Task IPostCommand.CreateCommentAsync(CreateCommentDto commentDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                //Load
                var post = await _postRepository.GetPost(commentDto.postID);
                
                // Do
                post.CreateComment(commentDto.text);

                // Save
                _postRepository.AddCommentAsync();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IPostCommand.UpdateCommentAsync(UpdateCommentDto commentDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                // Load
                var post = await _postRepository.GetPost(commentDto.postID);

                // Do
                var comment = post.UpdateComment(commentDto.commentID, commentDto.text);

                // Save
                _postRepository.UpdateCommentAsync(comment, commentDto.rowVersion);
                _unitOfWork.Commit();
            }
            catch (Exception)

            {
                _unitOfWork.Rollback();
                throw;
            }
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

    }
}
