using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Application.Repositories;

public interface IPostRepository
{
    Task AddPost(Post post);
    Task<Post> GetPost(int id);
    Task DeletePost(Post post);
}