using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;

namespace FMSEvaluering.Application.Commands.Interfaces;

public interface IPostCommand
{
    Task CreatePost(CreatePostDto postDto);
    Task DeletePost(DeletePostDto postDto);
    Task CreateVote(CreateVoteDto dto);
    Task DeleteVote(DeleteVoteDto dto);
    Task UpdateVote(UpdateVoteDto dto);
}