using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Domain.Entities;
using FMSEvaluering.Domain.Test.Fakes;

namespace FMSEvaluering.Domain.Test.VoteTests
{
    public class CreateVoteTests
    {
        [Fact]
        public void Given_Vote_Dont_Exist__Then_Create()
        {
            // Arrange
            var sut = new FakePost("1");

            sut.AddVote(new FakeVote(true, "2"));

            // Act & Assert
            sut.CreateVote(true, "3");
        }

        [Fact]
        public void Given_Vote_Already_Exist__Then_Throw()
        {
            // Arrange
            var sut = new FakePost("1");

            sut.AddVote(new FakeVote(true, "2"));

            // Act && Assert
            Assert.Throws<InvalidOperationException>(() => sut.CreateVote(true, "2"));
        }


    }
}
