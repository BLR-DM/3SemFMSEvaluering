using FMSEvaluering.Application.Queries.QueryDto;

namespace FMSEvaluering.Application.Helpers;

public interface ICsvGenerator
{
    Task<Stream> GenerateCsvForPosts(ForumDto forum);
}