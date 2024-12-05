using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreateCommentAsync(CreateCommentDto commentDto, string firstName, string lastName, int postId, string appUserId, string role, int forumId);
    Task UpdateCommentAsync(UpdateCommentDto commentDto, string appUserId, string role, int forumId);
    Task HandleVote(HandleVoteDto voteDto, string appUserId, string role, int forumId, int postId);
}