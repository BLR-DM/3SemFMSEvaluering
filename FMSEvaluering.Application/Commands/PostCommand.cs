using System.Net.Mail;
using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.MailService;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Application.Services;
using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Application.Commands;

public class PostCommand : IPostCommand
{
    private readonly IForumRepository _forumRepository;
    private readonly IForumAccessHandler _forumAccessHandler;
    private readonly INotificationService _notificationService;
    private readonly IUnitOfWork _unitOfWork;

    public PostCommand(IUnitOfWork unitOfWork, IForumRepository forumRepository, IForumAccessHandler forumAccessHandler,
        INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _forumRepository = forumRepository;
        _forumAccessHandler = forumAccessHandler;
        _notificationService = notificationService;
    }

    async Task IPostCommand.CreateCommentAsync(CreateCommentDto commentDto, string firstName, string lastName, int postId, string appUserId, string role, int forumId) // int postId?
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumWithSinglePostAsync(forumId, postId);

            // Validate Access
            await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);

            // Load
            var post = forum.GetPostById(postId);

            // Do
            post.CreateComment(firstName, lastName, commentDto.Text, appUserId); // string appUserId? + Navn?

            // Save 
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IPostCommand.UpdateCommentAsync(UpdateCommentDto commentDto, string appUserId, string role, int forumId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumWithSinglePostAsync(forumId, commentDto.PostId);

            // Validate Access
            await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);

            // Load
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

    async Task IPostCommand.HandleVote(HandleVoteDto voteDto, string appUserId, string role, int forumId, int postId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumWithSinglePostAsync(forumId, postId);

            // Validate Access
            await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);

            // Load
            var post = forum.GetPostById(postId);

            // Do
            var behaviour = post.HandleVote(voteDto.VoteType, appUserId);

            if (behaviour == Post.HandleVoteBehaviour.Update)
                _forumRepository.UpdateVote(post.Votes.First(v => v.AppUserId == appUserId), voteDto.RowVersion);
            else if (behaviour == Post.HandleVoteBehaviour.Delete)
            {
                _forumRepository.DeleteVote(post.Votes.First(v => v.AppUserId == appUserId), voteDto.RowVersion);
                post.DeleteVote(appUserId);
            }
            
            // Save
                await _unitOfWork.Commit();

            var upvotesCount = post.Votes.Count(v => v.VoteType);

            if (upvotesCount == 2)
            {
                _notificationService.NotifyTeacherOnPostDesiredLikes(forum, upvotesCount);
            }

        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}