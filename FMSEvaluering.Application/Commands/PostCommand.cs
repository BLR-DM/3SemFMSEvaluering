using FMSEvaluering.Application.Commands.CommandDto.PostDto;
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
}