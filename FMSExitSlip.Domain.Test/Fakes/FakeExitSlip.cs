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
        public FakeExitSlip(int maxQuestions, bool isPublished) : base()
        {
            MaxQuestions = maxQuestions;
            IsPublished = isPublished;
        }

        public new void EnsureExitSlipDoesntExceedMaxQuestions()
        {
            base.EnsureExitSlipDoesntExceedMaxQuestions();
        }

        public void AddQuestion(Question question)
        {
            _questions.Add(question); // Adgang til den beskyttede liste
        }
    }
}
