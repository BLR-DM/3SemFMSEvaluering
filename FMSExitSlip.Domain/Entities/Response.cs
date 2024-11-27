namespace FMSExitSlip.Domain.Entities;

public class Response : DomainEntity
{
    public string Text { get; protected set; }
    public string AppUserId { get; protected set; }

    protected Response() {}

    public Response(string text, string addUserId, IEnumerable<Response> responses)
    {
        Text = text;
        AppUserId = addUserId;

        AssureOnlyOneResponsePrQuestion(responses);
    }

    public static Response Create(string text, string appUserId, IEnumerable<Response> responses)
    {
        return new Response(text, appUserId, responses);
    }

    public void Update(string text)
    {
        Text = text;
    }

    private void AssureOnlyOneResponsePrQuestion(IEnumerable<Response> responses)
    {
        if (responses.Any(r => r.AppUserId == AppUserId))
            throw new InvalidOperationException("Only one answer to a question is allowed");
    }
}