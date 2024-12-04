namespace FMSExitSlip.Domain.Entities;

public class Response : DomainEntity
{
    public string Text { get; protected set; }
    public string AppUserId { get; protected set; }

    protected Response() {}

    public Response(string text, string appUserId, IEnumerable<Response> responses)
    {
        Text = text;
        AppUserId = appUserId;

        AssureOnlyOneResponsePrQuestion(responses);
    }

    public static Response Create(string text, string appUserId, IEnumerable<Response> responses)
    {
        return new Response(text, appUserId, responses);
    }

    public void Update(string text, string appUserId)
    {
        AssureUserIsCreator(appUserId);
        Text = text;
    }

    public void Delete(string appUserId)
    {
        AssureUserIsCreator(appUserId);
    }

    protected void AssureUserIsCreator(string appUserId)
    {
        if (!AppUserId.Equals(appUserId))
            throw new ArgumentException("Only the creater of the response can edit it");
    }

    protected void AssureOnlyOneResponsePrQuestion(IEnumerable<Response> responses)
    {
        if (responses.Any(r => r.AppUserId == AppUserId))
            throw new InvalidOperationException("Only one answer to a question is allowed");
    }
}