using FMSEvaluering.Application.Commands.CommandDto.ForumDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IForumCommand
{
    Task CreatePublicForumAsync(CreatePublicForumDto forumDto);
    Task CreateClassForumAsync(CreateClassForumDto forumDto);
    Task CreateSubjectForumAsync(CreateSubjectForumDto forumDto);
    Task AddPost(CreatePostDto postDto, int forumId);

    Task DeleteForumAsync(DeleteForumDto forumDto);
}