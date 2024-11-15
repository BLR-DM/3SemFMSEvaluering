using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreatePost(CreatePostDto postDto);
    Task DeletePost(DeletePostDto postDto);
    Task CreateVote(CreateVoteDto voteDto);
    Task DeleteVote(DeleteVoteDto voteDto);
    Task UpdateVote(UpdateVoteDto voteDto);
    Task CreateCommentAsync(CreateCommentDto commentDto);
    Task UpdateCommentAsync(UpdateCommentDto commentDto);
}