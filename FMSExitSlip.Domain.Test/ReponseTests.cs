using FMSExitSlip.Domain.Test.Fakes;

namespace FMSExitSlip.Domain.Test;

public class ReponseTests
{
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
}