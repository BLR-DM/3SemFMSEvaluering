using FMSEvaluering.Application.Queries.Interfaces;

namespace FMSEvaluering.Application.Helpers;

public interface IGenerateCsvHandler
{
    Task<Stream> HandlePostsAsync(int forumId, string appUserId, string role, DateOnly fromDate, DateOnly toDate, int reqUpvotes);
}

public class GenerateCsvHandler : IGenerateCsvHandler
{
    private readonly IForumQuery _query;
    private readonly ICsvGenerator _csvGenerator;

    public GenerateCsvHandler(IForumQuery query, ICsvGenerator csvGenerator)
    {
        _query = query;
        _csvGenerator = csvGenerator;
    }
    async Task<Stream> IGenerateCsvHandler.HandlePostsAsync(int forumId, string appUserId, string role, DateOnly fromDate, DateOnly toDate, int reqUpvotes)
    {
        var forum = await _query.GetForumWithPostsByDateRange(forumId, appUserId, role, fromDate, toDate, reqUpvotes);

        if (forum == null)
        {
            throw new InvalidOperationException("Forum not found");
        }

        return await _csvGenerator.GenerateCsvForPosts(forum);
    }
}