using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Domain.Entities
{
    public class ExitSlip : DomainEntity
    {
        private readonly List<Question> _questions = [];
        public string Title { get; protected set; }
        public int MaxQuestions { get; protected set; }
        public bool IsPublished { get; protected set; } = false;
        public int LectureId { get; protected set; }

        public ICollection<Question> Questions => _questions;

        public void CreateQuestion(string text, string appUserId)
        {
            EnsureExitSlipDoesntExceedMaxQuestions();
            EnsureExitSlipIsNotPublished();

            _questions.Add(Question.Create(text, appUserId));
        }

        public void EnsureExitSlipIsNotPublished()
        {
            if (IsPublished)
                throw new InvalidOperationException("Cannot add questions to a published exitslip");
        }

        public void EnsureExitSlipDoesntExceedMaxQuestions()
        {
            if (Questions.Count >= MaxQuestions)
                throw new InvalidOperationException($"Cannot add more than {MaxQuestions} questions");
        }
    }
}
