using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Domain.Values;

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

    async Task IPostCommand.CreatePostAsync(CreatePostDto postDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Do
            var post = Post.Create(postDto.Description, postDto.Solution, postDto.AppUserÌd);

            // Save
            await _postRepository.AddPost(post);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.AddPostHistory(UpdatePostDto updatePostDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var post = await _postRepository.GetPost(updatePostDto.PostId);
            
            // Do
            post.SetPostHistory(new PostHistory(updatePostDto.Content));

            // Save
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }

    }

    async Task IPostCommand.DeletePostAsync(DeletePostDto postDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var post = await _postRepository.GetPost(postDto.Id);

            // Do & Save
            _postRepository.DeletePost(post);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }




    //async Task IPostCommand.CreateVote(CreateVoteDto voteDto)
    //{
    //    try
    //    {
    //        await _unitOfWork.BeginTransaction();

    //        // Load 
    //        var post = await _postRepository.GetPost(voteDto.PostId);

    //        // Do
    //        post.CreateVote(voteDto.VoteType);

    //        // Save
    //        await _unitOfWork.Commit();
    //    }
    //    catch (Exception)
    //    {
    //        await _unitOfWork.Rollback();
    //        throw;
    //    }
    //}

    //async Task IPostCommand.UpdateVote(UpdateVoteDto voteDto)
    //{
    //    try
    //    {
    //        await _unitOfWork.BeginTransaction();

    //        // Load
    //        var post = await _postRepository.GetPost(voteDto.PostId);

    //        // Do
    //        post.UpdateVote(voteDto.Id, voteDto.VoteType);

    //        // Save
    //        await _unitOfWork.Commit();
    //    }
    //    catch (Exception)
    //    {
    //        await _unitOfWork.Rollback();
    //        throw;
    //    }
    //}

    //async Task IPostCommand.DeleteVote(DeleteVoteDto voteDto)
    //{
    //    try
    //    {
    //        await _unitOfWork.BeginTransaction();
    //        // Load 
    //        var post = await _postRepository.GetPost(voteDto.PostId);
    //        // Do
    //        post.DeleteVote(voteDto.Id);
    //        // Save
    //        await _unitOfWork.Commit();
    //    }
    //    catch (Exception)
    //    {
    //        await _unitOfWork.Rollback();
    //        throw;
    //    }
    //}

    async Task IPostCommand.CreateCommentAsync(CreateCommentDto commentDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var post = await _postRepository.GetPost(commentDto.postID);

            // Do
            post.CreateComment(commentDto.text);

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
            var post = await _postRepository.GetPost(commentDto.postID);

            // Do
            var comment = post.UpdateComment(commentDto.commentID, commentDto.text);

            // Save
            _postRepository.UpdateCommentAsync(comment, commentDto.rowVersion);
            await _unitOfWork.Commit();

        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.HandleVote(CreateVoteDto voteDto, string appUserId, int postId)
    {   
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var post = await _postRepository.GetPost(postId);

            // Do
            post.HandleVote(voteDto.VoteType, appUserId);

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