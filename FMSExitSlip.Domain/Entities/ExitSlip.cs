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
        public string AppUserId { get; protected set; }
        public int LectureId { get; protected set; }

        public ICollection<Question> Questions => _questions;
        
        protected ExitSlip() { }

        private ExitSlip(string title, int maxQuestions, bool isPublished, string appUserId, int lectureId, IEnumerable<ExitSlip> otherExitSlips)
        {
            Title = title;
            MaxQuestions = maxQuestions;
            AppUserId = appUserId;
            LectureId = lectureId;

            AssureOnlyOneExitSlipPrLesson(otherExitSlips);
        }

        public static ExitSlip Create(string title, int maxQuestions, bool isPublished, int lectureId, string appUserId, IEnumerable<ExitSlip> otherExitSlips)
        {
            return new ExitSlip(title, maxQuestions, isPublished, appUserId, lectureId, otherExitSlips);
        }

        private void AssureOnlyOneExitSlipPrLesson(IEnumerable<ExitSlip> otherExitSlips)
        {
            if (otherExitSlips.Any(l => l.LectureId == LectureId))
            {
                throw new Exception("Only one exit slip per lesson is allowed.");
            }

        }
        
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
