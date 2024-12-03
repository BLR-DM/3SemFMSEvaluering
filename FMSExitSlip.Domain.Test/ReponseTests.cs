using FMSExitSlip.Domain.Entities;
using FMSExitSlip.Domain.Test.Fakes;

namespace FMSExitSlip.Domain.Test;

public class ReponseTests
{
    // AssureUserIsCreator

    [Theory]
    [InlineData("1", "1")]
    public void Given_User_Is_Same_As_Creator__Then_Dont_Throw(string appUserIdCreator, string appUserId)
    {
        // Arrange
        var sut = new FakeResponse("text", appUserIdCreator);

        // Act & Assert
        sut.AssureUserIsCreator(appUserId);
    }

    [Theory]
    [InlineData("1", "2")]
    public void Given_User_Is_Not_Same_As_Creator__Then_Throw(string appUserIdCreator, string appUserId)
    {
        // Arrange
        var sut = new FakeResponse("text", appUserIdCreator);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => sut.AssureUserIsCreator(appUserId));
    }

    // AssureOnlyOneResponsePrQuestion

    [Theory]
    [MemberData(nameof(ResponseTestWithOnlyOnePrQuestion))]
    public void Given_Only_One_Response_For_Question__Then_Dont_Throw(Response response, IEnumerable<Response> responses)
    {
        // Arrange
        var sut = new FakeResponse(response.Text, response.AppUserId);

        // Act & Assert
        sut.AssureOnlyOneResponsePrQuestion(responses);
    }

    [Theory]
    [MemberData(nameof(ResponseWithMoreThanOnePrQuestion))]
    public void Given_More_Than_One_Response_Pr_Question__Then_Throw(Response response, IEnumerable<Response> responses)
    {
        // Arrange
        var sut = new FakeResponse(response.Text, response.AppUserId);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => sut.AssureOnlyOneResponsePrQuestion(responses));
    }



    public static IEnumerable<object[]> ResponseTestWithOnlyOnePrQuestion()
    {
        var responses = CreateResponses();
        yield return
        [
            new FakeResponse("text","4"),
            responses
        ];

        yield return
        [
            new FakeResponse("text","5"),
            responses
        ];

        yield return
        [
            new FakeResponse("text","6"),
            responses
        ];
    }

    public static IEnumerable<object[]> ResponseWithMoreThanOnePrQuestion()
    {
        var responses = CreateResponses();

        yield return
        [
            new FakeResponse("text","1"),
            responses
        ];

        yield return
        [
            new FakeResponse("text","2"),
            responses
        ];

        yield return
        [
            new FakeResponse("text","3"),
            responses
        ];
    }

    public static List<Response> CreateResponses()
    {
        var responses = new List<Response>(new[]
        {
        new FakeResponse("text","1"),
        new FakeResponse("text","2"),
        new FakeResponse("text","3")
    });
        return responses;
    }
}