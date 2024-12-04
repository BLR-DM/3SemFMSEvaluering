using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.Entities;
using FMSExitSlip.Domain.Test.Fakes;
using Xunit;

namespace FMSExitSlip.Domain.Test
{
    public class QuestionTests
    {
        [Theory]
        [InlineData(3, 2)]
        [InlineData(5, 1)] 
        [InlineData(4, 0)]
        public void Given_Questions_Dont_Exceed_Maximum__Then_Dont_Throw(int maxQuestions, int currentQuestions)
        {
            // Arrange
            var sut = new FakeExitSlip(maxQuestions, false, "test", 1);

            for (int i = 0; i < currentQuestions; i++)
            {
                sut.AddQuestion(new FakeQuestion());
            }

            // Act & Assert
            sut.EnsureExitSlipDoesntExceedMaxQuestions();
        }

        [Theory]
        [InlineData(3, 5)] 
        [InlineData(5, 7)]
        [InlineData(4, 9)]
        public void Given_Questions_Do_Exceed_Maximum__Then_ThrowsInvalidOperationException(int maxQuestions, int currentQuestions)
        {
            // Arrange
            var sut = new FakeExitSlip(maxQuestions, false,"test", 2);

            for (int i = 0; i < currentQuestions; i++)
            {
                sut.AddQuestion(new FakeQuestion());
            }

            // Act & Assert

            Assert.Throws<InvalidOperationException>(() => sut.EnsureExitSlipDoesntExceedMaxQuestions());
        }


        [Fact]
        public void Given_Exitslip_Is_Not_Published__Then_Dont_Throw()
        {
            // Arrange
            var sut = new FakeExitSlip(5,false, "test", 3); 

            // Act & Assert
            sut.EnsureExitSlipIsNotPublished();
        }

        [Fact]
        public void Given_ExitSlip_Is_Published__Then_ThrowsInvalidOperationException()
        {
            // Arrange
            var sut = new FakeExitSlip(5,true,"test", 4);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.EnsureExitSlipIsNotPublished());
        }

    }
}
