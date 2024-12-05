using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreateCommentAsync(CreateCommentDto commentDto, int forumId);
    Task UpdateCommentAsync(UpdateCommentDto commentDto, int forumId);
    Task HandleVote(HandleVoteDto voteDto, string appUserId, int forumId, int postId);
}