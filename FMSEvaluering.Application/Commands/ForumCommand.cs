using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Commands.CommandDto.ForumDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities.Forum;

namespace FMSEvaluering.Application.Commands
{
    public class ForumCommand : IForumCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IForumRepository _forumRepository;

        public ForumCommand(IUnitOfWork unitOfWork, IForumRepository forumRepository)
        {
            _unitOfWork = unitOfWork;
            _forumRepository = forumRepository;
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
                var forum = await _forumRepository.GetForum(forumDto.Id);

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
