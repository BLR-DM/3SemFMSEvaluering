namespace FMSExitSlip.Domain.Entities;

public class Question : DomainEntity
{
    private readonly List<Response> _responses = [];

    protected Question()
    {
    }

    private Question(string text, string appUserId)
    {
        Text = text;
        AppUserId = appUserId;
    }

    public string Text { get; protected set; }
    public string AppUserId { get; protected set; }
    public IReadOnlyCollection<Response> Responses => _responses;

    public static Question Create(string text, string appUserId)
    {
        return new Question(text, appUserId);
    }

    public void Update(string text)
    {
        Text = text;
    }

    public void AddResponse(string text, string appUserId)
    {
        var response = Response.Create(text, appUserId, Responses);
        _responses.Add(response);
    }

    public Response UpdateResponse(int responseId, string text, string appUserId)
    {
        var response = GetResponseById(responseId);
        response.Update(text, appUserId);
        return response;
    }

    public Response DeleteResponse(int responseId, string appUserId)
    {
        var response = GetResponseById(responseId);
        response.Delete(appUserId);
        _responses.Remove(response);
        return response;
    }

    private Response GetResponseById(int responseId)
    {
        var response = Responses.FirstOrDefault(r => r.Id == responseId);
        return response ?? throw new ArgumentException("Response not found");
    }
}