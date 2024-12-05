using FMSEvaluering.Application.Commands.CommandDto.ForumDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Application.Commands;

public class ForumCommand : IForumCommand
{
    private readonly IForumRepository _forumRepository;
    private readonly IForumAccessHandler _forumAccessHandler;
    private readonly IUnitOfWork _unitOfWork;

    public ForumCommand(IUnitOfWork unitOfWork, IForumRepository forumRepository, IForumAccessHandler forumAccessHandler)
    {
        _unitOfWork = unitOfWork;
        _forumRepository = forumRepository;
        _forumAccessHandler = forumAccessHandler;
    }

    async Task IForumCommand.CreatePostAsync(CreatePostDto postDto, string appUserId, string role, int forumId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumAsync(forumId);

            // Validate Access
            await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);
            
            // Do
            await forum.AddPostAsync(postDto.Description, postDto.Solution, appUserId);

            //Save
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IForumCommand.UpdatePostAsync(UpdatePostDto postDto, string appUserId, string role, int postId, int forumId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumAsync(forumId);

            // Validate Access
            await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);

            // Do
            var post = await forum.UpdatePostAsync(postId, postDto.Description, postDto.Solution,
                appUserId);
            _forumRepository.UpdatePost(post, postDto.RowVersion);

            //Save
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IForumCommand.DeletePostAsync(DeletePostDto postDto, string appUserId, string role, int postId, int forumId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumAsync(forumId);

            // Validate Access
            await _forumAccessHandler.ValidateAccessSingleForumAsync(appUserId, role, forum);

            // Do
            var post = await forum.DeletePostAsync(postId, appUserId);
            _forumRepository.DeletePost(post, postDto.RowVersion);

            //Save
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IForumCommand.CreatePublicForumAsync(CreatePublicForumDto forumDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Do
            var forum = Forum.CreatePublicForum(forumDto.Name);
            await _forumRepository.AddForum(forum);

            // Save
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IForumCommand.CreateClassForumAsync(CreateClassForumDto forumDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // do
            var forum = Forum.CreateClassForum(forumDto.Name, forumDto.ClassId);
            await _forumRepository.AddForum(forum);

            // Save
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IForumCommand.CreateSubjectForumAsync(CreateSubjectForumDto forumDto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Do
            var forum = Forum.CreateSubjectForum(forumDto.Name, forumDto.SubjectId);
            await _forumRepository.AddForum(forum);

            // Save
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }

    async Task IForumCommand.DeleteForumAsync(DeleteForumDto forumDto, int forumId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            // Load
            var forum = await _forumRepository.GetForumAsync(forumId);

            // Do
            _forumRepository.DeleteForum(forum, forumDto.RowVersion);

            // Save
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}