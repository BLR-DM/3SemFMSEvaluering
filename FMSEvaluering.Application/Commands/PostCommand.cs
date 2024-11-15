﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;

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

        async Task IPostCommand.CreatePost(CreatePostDto dto)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                // Do
                var post = Post.Create(dto.description);

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
