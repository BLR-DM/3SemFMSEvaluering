using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Application.Repositories;

public interface IPostRepository
{
    Task AddPost(Post post);
    Task<Post> GetPost(int id);
    //Task AddPostHistory(Post post);
    void DeletePost(Post post);
    //Task AddVote();
    //Task DeleteVote();
    //Task UpdateVote();
    //Task AddCommentAsync();
    void UpdateCommentAsync(Comment comment, byte[] rowVersion);
}