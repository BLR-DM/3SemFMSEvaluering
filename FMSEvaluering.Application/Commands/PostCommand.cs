using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Application.Commands;

public class PostCommand : IPostCommand
{
    private readonly IPostRepository _postRepository;
    private readonly IForumRepository _forumRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PostCommand(IUnitOfWork unitOfWork, IPostRepository postRepository, IForumRepository forumRepository)
    {
        _unitOfWork = unitOfWork;
        _postRepository = postRepository;
        _forumRepository = forumRepository;
    }

    async Task IPostCommand.CreatePostAsync(CreatePostDto postDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForum(int.Parse(postDto.ForumId));

            // Do
            var post = Post.Create(postDto.Description, postDto.Solution, postDto.AppUserId, forum);
            await _postRepository.AddPost(post);
            
            // Save
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.UpdatePost(UpdatePostDto updatePostDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var post = await _postRepository.GetPost(updatePostDto.PostId);
            
            // Do
            post.Update(updatePostDto.Content, updatePostDto.AppUserId);
            _postRepository.UpdatePost(post, updatePostDto.RowVersion);

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