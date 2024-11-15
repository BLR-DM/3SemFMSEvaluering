using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreatePost(CreatePostDto postDto);
    Task DeletePost(DeletePostDto postDto);
}