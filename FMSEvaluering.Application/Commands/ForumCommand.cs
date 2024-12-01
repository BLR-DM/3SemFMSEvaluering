﻿using FMSEvaluering.Application.Commands.CommandDto.ForumDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.DomainServices;
using FMSEvaluering.Domain.Entities.ForumEntities;

namespace FMSEvaluering.Application.Commands
{
    public class ForumCommand : IForumCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IForumRepository _forumRepository;
        private readonly IServiceProvider _serviceProvider;

        public ForumCommand(IUnitOfWork unitOfWork, IForumRepository forumRepository, IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _forumRepository = forumRepository;
            _serviceProvider = serviceProvider;
        }
        async Task IForumCommand.AddPost(CreatePostDto postDto, int forumId)
        {
            var forum = await _forumRepository.GetForumAsync(forumId);


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

        async Task IForumCommand.DeleteForumAsync(DeleteForumDto forumDto)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var forum = await _forumRepository.GetForumAsync(forumDto.Id);

                // Do
                _forumRepository.DeleteForum(forum);

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
}
