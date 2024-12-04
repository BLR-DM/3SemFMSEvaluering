using FMSExitSlip.Domain.Entities;
using FMSExitSlip.Domain.Test.Fakes;

namespace FMSExitSlip.Domain.Test;

public class OneExitSlipPrLessonTest
{
    [Theory]
    [MemberData(nameof(ExitSlipTestWithOnlyOnePrLesson))]
    public void Given_Only_One_Exitslip_For_Lesson__Then_Dont_Throw(ExitSlip exitslip, List<ExitSlip> otherExitSlips)
    {
        // Arrange
        var sut = new FakeExitSlip(exitslip.MaxQuestions, exitslip.IsPublished, exitslip.AppUserId, exitslip.LectureId);

        // Act & Assert
        sut.AssureOnlyOneExitSlipPrLesson(otherExitSlips);
    }

    [Theory]
    [MemberData(nameof(ExitSlipWithMoreThanOnePrLesson))]
    public void Given_More_Than_One_ExitSlip_Pr_Lesson__Then_Throw(ExitSlip exitSlip, List<ExitSlip> otherExitSlips)
    {
        // Arrange
        var sut = new FakeExitSlip(exitSlip.MaxQuestions, exitSlip.IsPublished, exitSlip.AppUserId, exitSlip.LectureId);

        // Act & Assert
        Assert.Throws<Exception>(() => sut.AssureOnlyOneExitSlipPrLesson(otherExitSlips));
    }



    public static IEnumerable<object[]> ExitSlipTestWithOnlyOnePrLesson()
    {
        var otherExitSlips = CreateOtherExitSlips();
        yield return
        [
            new FakeExitSlip(5, false, "test", 1),
            otherExitSlips
        ];

        yield return
        [
            new FakeExitSlip(5, false, "test", 4),
            otherExitSlips
        ];

        yield return
        [
            new FakeExitSlip(5, false, "test", 6),
            otherExitSlips
        ];
    }

    public static IEnumerable<object[]> ExitSlipWithMoreThanOnePrLesson()
    {
        var otherExitSlips = CreateOtherExitSlips();

        yield return
        [
            new FakeExitSlip(5, false, "test", 2),
            otherExitSlips
        ];

        yield return
        [
            new FakeExitSlip(5, false, "test", 3),
            otherExitSlips
        ];

        yield return
        [
            new FakeExitSlip(5, false, "test", 5),
            otherExitSlips
        ];
    }

    public static List<ExitSlip> CreateOtherExitSlips()
    {
        var otherExitSlips = new List<ExitSlip>(new[]
        {
            new FakeExitSlip(5, false, "test", 2),
            new FakeExitSlip(5, false, "test", 3),
            new FakeExitSlip(5, false, "test", 5)
        });
        return otherExitSlips;
    }
}