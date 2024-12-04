using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.Test.Fakes;
using Xunit;

namespace FMSExitSlip.Domain.Test
{
    public class EnsureTeacherSameAsCreaterTest
    {
        [Fact] 
        public void Given_Teacher_Is_Same_As_Creater__Then_Dont_Throw()
        {
            // Arrange
            var sut = new FakeExitSlip(5, false, "test123", 1);

            // Act & Assert
            sut.EnsureTeacherSameAsCreator("test123");
        }

        [Theory]
        [InlineData("test12")]
        [InlineData("test1234")]
        public void Given_Teacher_Is_Not_The_Same_As_Creater__Then_ThrowsInvalidOperationException(string appUserId)
        {
            // Arrange
            var sut = new FakeExitSlip(5, false, "test123", 1);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.EnsureTeacherSameAsCreator(appUserId));
        }
    }
}
