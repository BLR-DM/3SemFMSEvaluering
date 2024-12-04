using FMSExitSlip.Domain.Entities;

namespace FMSExitSlip.Domain.Test.Fakes;

public class FakeResponse : Response
{
    public FakeResponse(string text, string appUserId) : base()
    {
        Text = text;
        AppUserId = appUserId;
    }

    public new void AssureUserIsCreator(string appUserId)
    {
        base.AssureUserIsCreator(appUserId);
    }

    public new void AssureOnlyOneResponsePrQuestion(IEnumerable<Response> responses)
    {
        base.AssureOnlyOneResponsePrQuestion(responses);
    }
}