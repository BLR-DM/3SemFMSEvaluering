using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Application.Repositories;

public interface IPostRepository
{
    Task<Post> GetPostAsync(int id);
    void UpdateComment(Comment comment, byte[] rowVersion);
    void UpdateVote(Vote vote, byte[] rowVersion);
    void DeleteVote(Vote vote, byte[] rowVersion);
}