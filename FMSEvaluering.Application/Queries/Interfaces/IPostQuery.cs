using FMSEvaluering.Application.Queries.QueryDto;

namespace FMSEvaluering.Application.Queries.Interfaces;

public interface IPostQuery
{
    Task<PostDto> GetPost(int postId);
}