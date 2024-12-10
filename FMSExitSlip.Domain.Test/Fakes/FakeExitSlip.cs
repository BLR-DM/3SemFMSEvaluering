using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSExitSlip.Domain.Entities;

namespace FMSExitSlip.Domain.Test.Fakes
{
    public class FakeExitSlip : ExitSlip
    {
        public FakeExitSlip(int maxQuestions, bool isPublished, int lectureId) : base()
        {
            MaxQuestions = maxQuestions;
            IsPublished = isPublished;
            LectureId = lectureId;
        }

        public new void EnsureExitSlipDoesntExceedMaxQuestions()
        {
            base.EnsureExitSlipDoesntExceedMaxQuestions();
        }

        public new void EnsureExitSlipIsNotPublished()
        {
            base.EnsureExitSlipIsNotPublished();
        }

        public new void AssureOnlyOneExitSlipPrLesson(IEnumerable<ExitSlip> otherExitSlips)
        {
            base.AssureOnlyOneExitSlipPrLesson(otherExitSlips);
        }

        public void AddQuestion(Question question)
        {
            _questions.Add(question); // Adgang til den beskyttede liste
        }
    }
}
