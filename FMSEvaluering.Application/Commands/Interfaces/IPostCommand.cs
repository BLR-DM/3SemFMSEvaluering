using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreateCommentAsync(CreateCommentDto commentDto);
    Task UpdateCommentAsync(UpdateCommentDto commentDto);
    Task HandleVote(CreateVoteDto voteDto, string appUserId, int postId);
}