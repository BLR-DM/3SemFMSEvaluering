using FMSEvaluering.Application.Commands.CommandDto.ForumDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IForumCommand
{
    Task CreatePublicForumAsync(CreatePublicForumDto forumDto);
    Task CreateClassForumAsync(CreateClassForumDto forumDto);
    Task CreateSubjectForumAsync(CreateSubjectForumDto forumDto);
    Task DeleteForumAsync(DeleteForumDto forumDto, int forumId);
    Task CreatePostAsync(CreatePostDto postDto, string appUserId, string role, int forumId);
    Task UpdatePostAsync(UpdatePostDto postDto, string appUserId, string role, int postId, int forumId);
    Task DeletePostAsync(DeletePostDto postDto, string appUserId, string role, int postId, int forumId);
}