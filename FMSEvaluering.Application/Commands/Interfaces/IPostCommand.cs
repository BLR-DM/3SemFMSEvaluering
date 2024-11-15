using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreatePost(CreatePostDto dto);
    Task CreateCommentAsync(CreateCommentDto commentDto);
    Task UpdateCommentAsync(UpdateCommentDto commentDto);
}