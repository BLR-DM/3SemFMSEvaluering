using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Application.Repositories;

public interface IPostRepository
{
    Task AddPost(Post post);
    Task<Post> GetPost(int id);
    Task AddPostHistory(Post post);
    Task DeletePost(Post post);
    Task AddVote();
    Task DeleteVote();
    Task UpdateVote();
    Task AddCommentAsync();
    Task UpdateCommentAsync(Comment comment, byte[] rowVersion);
}