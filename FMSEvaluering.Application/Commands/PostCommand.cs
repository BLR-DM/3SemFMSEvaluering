﻿using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;

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

    async Task IPostCommand.CreatePost(CreatePostDto postDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            // Do
            var post = Post.Create(postDto.Description, postDto.Solution);

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

    async Task IPostCommand.DeletePost(DeletePostDto postDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            // Do
            var post = await _postRepository.GetPost(postDto.Id);

            // Save
            await _postRepository.DeletePost(post);

            // Commit
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.CreateVote(CreateVoteDto voteDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            // Load 
            var post = await _postRepository.GetPost(voteDto.PostId);

            // Do
            post.CreateVote(voteDto.VoteType);

            // Save
            _postRepository.AddVote();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.UpdateVote(UpdateVoteDto voteDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            // Load
            var post = await _postRepository.GetPost(voteDto.PostId);

            // Do
            post.UpdateVote(voteDto.Id, voteDto.VoteType);

            // Save
            await _postRepository.UpdateVote();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.DeleteVote(DeleteVoteDto voteDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            // Load 
            var post = await _postRepository.GetPost(voteDto.PostId);
            // Do
            post.DeleteVote(voteDto.Id);
            // Save
            await _postRepository.DeleteVote();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.CreateCommentAsync(CreateCommentDto commentDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            // Load
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
}