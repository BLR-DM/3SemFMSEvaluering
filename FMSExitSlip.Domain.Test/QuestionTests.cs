using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.Entities;
using FMSExitSlip.Domain.Test.Fakes;

namespace FMSExitSlip.Domain.Test
{
    public class QuestionTests
    {
        [Theory]
        [InlineData(3, 2)] // Maks ikke overskredet
        [InlineData(5, 1)] // Maks ikke overskredet
        [InlineData(4, 0)] // Ingen spørgsmål endnu
        public void Given_Questions_Dont_Exceed_Maximum__Then_Dont_Throw(int maxQuestions, int currentQuestions)
        {
            // Arrange
            var sut = new FakeExitSlip(maxQuestions, false);

            for (int i = 0; i < currentQuestions; i++)
            {
                sut.AddQuestion(new FakeQuestion());
            }

            // Act & Assert
            sut.EnsureExitSlipDoesntExceedMaxQuestions();
        }

        [Theory]
        [InlineData(3, 5)] // Maks ikke overskredet
        [InlineData(5, 7)] // Maks ikke overskredet
        [InlineData(4, 9)] // Ingen spørgsmål endnu
        public void Given_Questions_Do_Exceed_Maximum__Then_ThrowsInvalidOperationException(int maxQuestions, int currentQuestions)
        {
            // Arrange
            var sut = new FakeExitSlip(maxQuestions, false);

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
            var sut = new FakeExitSlip(5,false); // Ikke udgivet

            // Act & Assert
            sut.EnsureExitSlipIsNotPublished();


        }

        [Fact]
        public void Given_ExitSlip_Is_Published__Then_ThrowsInvalidOperationException()
        {
            // Arrange
            var sut = new FakeExitSlip(5,true); // Simulerer en udgivelse

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.EnsureExitSlipIsNotPublished());

        }

       


    }
}
