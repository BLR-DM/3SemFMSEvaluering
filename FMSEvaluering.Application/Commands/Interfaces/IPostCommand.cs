using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreatePostAsync(CreatePostDto postDto, string appUserId, int forumId);
    Task UpdatePost(UpdatePostDto updatePostDto, string appUserId);
    Task DeletePostAsync(DeletePostDto postDto);
    //Task CreateVote(CreateVoteDto voteDto);
    //Task DeleteVote(DeleteVoteDto voteDto);
    //Task UpdateVote(UpdateVoteDto voteDto);
    Task CreateCommentAsync(CreateCommentDto commentDto);
    Task UpdateCommentAsync(UpdateCommentDto commentDto);
    Task HandleVote(CreateVoteDto voteDto, string appUserId, int postId);
}