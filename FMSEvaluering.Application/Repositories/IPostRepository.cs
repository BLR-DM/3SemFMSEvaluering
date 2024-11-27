using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Application.Repositories;

public interface IPostRepository
{
    Task AddPost(Post post);
    Task<Post> GetPostAsync(int id);
    void UpdatePost(Post post, byte[] rowVersion);

    void DeletePost(Post post, byte[] rowVersion);
    //Task AddVote();
    //Task DeleteVote();
    //Task UpdateVote();
    //Task AddCommentAsync();
    void UpdateCommentAsync(Comment comment, byte[] rowVersion);
}