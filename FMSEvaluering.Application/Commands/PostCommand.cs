﻿using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Application.Commands;

public class PostCommand : IPostCommand
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PostCommand(IUnitOfWork unitOfWork, IPostRepository postRepository)
    {
        _unitOfWork = unitOfWork;
        _postRepository = postRepository;
    }

    async Task IPostCommand.CreateCommentAsync(CreateCommentDto commentDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var post = await _postRepository.GetPostAsync(commentDto.PostId);

            // Do
            post.CreateComment(commentDto.Text);

            // Save 
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.UpdateCommentAsync(UpdateCommentDto commentDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var post = await _postRepository.GetPostAsync(commentDto.PostId);

            // Do
            var comment = post.UpdateComment(commentDto.CommentId, commentDto.Text);

            // Save
            _postRepository.UpdateComment(comment, commentDto.RowVersion);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.HandleVote(HandleVoteDto voteDto, string appUserId, int postId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var post = await _postRepository.GetPostAsync(postId);

            // Do
            var behaviour = post.HandleVote(voteDto.VoteType, appUserId);

            if (behaviour == Post.HandleVoteBehaviour.Update)
            {
                _postRepository.UpdateVote(post.Votes.First(v => v.AppUserId == appUserId), voteDto.RowVersion);
            } 
            else if (behaviour == Post.HandleVoteBehaviour.Delete)
            {
                _postRepository.DeleteVote(post.Votes.First(v => v.AppUserId == appUserId), voteDto.RowVersion);
            }

            // Save
            await _unitOfWork.Commit();
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}