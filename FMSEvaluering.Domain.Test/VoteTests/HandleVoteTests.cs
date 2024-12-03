using FMSEvaluering.Domain.Entities.PostEntities;
using FMSEvaluering.Domain.Test.Fakes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

namespace FMSEvaluering.Domain.Test.VoteTests;

public class HandleVoteTests
{
    [Fact]
    public void Given_UserHasNotVotedBefore__When_HandlingVote__Then_CreatesVote()
    {
        // Arrange
        var sut = new FakePost("1");

        // Act
        var result = sut.HandleVote(true, "2");

        // Assert
        Assert.Single(sut.Votes);
        Assert.True(sut.Votes.First().VoteType);
        Assert.Equal(Post.HandleVoteBehaviour.Create, result);
    }

    [Fact]
    public void Given_UserHasVotedSameVoteBefore__When_HandlingVote__Then_DeletesVote()
    {
        // Arrange
        var sut = new FakePost("1");

        sut.AddVote(new FakeVote(true, "2"));
        // Act
        var result = sut.HandleVote(true, "2");

        // Assert
        Assert.Empty(sut.Votes);
        Assert.Equal(Post.HandleVoteBehaviour.Delete, result);
    }

    [Fact]
    public void Given_UserHasVotedDifferentVoteBefore__When_HandlingVote__Then_UpdatesVote()
    {
        // Arrange
        var sut = new FakePost("1");

        sut.AddVote(new FakeVote(true, "2"));

        // Act
        var result = sut.HandleVote(false, "2");

        // Assert
        Assert.Single(sut.Votes);
        Assert.False(sut.Votes.First().VoteType); 
        Assert.Equal(Post.HandleVoteBehaviour.Update, result);
    }
}