using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Application.Commands;

public class PostCommand : IPostCommand
{
    private readonly IForumRepository _forumRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PostCommand(IUnitOfWork unitOfWork, IForumRepository forumRepository)
    {
        _unitOfWork = unitOfWork;
        _forumRepository = forumRepository;
    }

    async Task IPostCommand.CreateCommentAsync(CreateCommentDto commentDto, int forumId) // int postId?
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumWithSinglePostAsync(forumId, commentDto.PostId);
            // Check / Validation <- AccessHandler
            var post = forum.GetPostById(commentDto.PostId);

            // Do
            post.CreateComment(commentDto.Text); // string appUserId? + Navn?

            // Save 
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.UpdateCommentAsync(UpdateCommentDto commentDto, int forumId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumWithSinglePostAsync(forumId, commentDto.PostId);
            // Check / Validation <- AccessHandler
            var post = forum.GetPostById(commentDto.PostId);

            // Do
            var comment = post.UpdateComment(commentDto.CommentId, commentDto.Text);

            // Save
            _forumRepository.UpdateComment(comment, commentDto.RowVersion);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.HandleVote(HandleVoteDto voteDto, string appUserId, int forumId, int postId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumWithSinglePostAsync(forumId, postId);
            // Check / Validation <- AccessHandler
            var post = forum.GetPostById(postId);

            // Do
            var behaviour = post.HandleVote(voteDto.VoteType, appUserId);

            if (behaviour == Post.HandleVoteBehaviour.Update)
                _forumRepository.UpdateVote(post.Votes.First(v => v.AppUserId == appUserId), voteDto.RowVersion);
            else if (behaviour == Post.HandleVoteBehaviour.Delete)
                _forumRepository.DeleteVote(post.Votes.First(v => v.AppUserId == appUserId), voteDto.RowVersion);

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