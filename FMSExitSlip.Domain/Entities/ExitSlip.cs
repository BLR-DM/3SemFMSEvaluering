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

        private ExitSlip(string title, int maxQuestions, string appUserId, int lectureId, IEnumerable<ExitSlip> otherExitSlips)
        {
            Title = title;
            MaxQuestions = maxQuestions;
            AppUserId = appUserId;
            LectureId = lectureId;

            AssureOnlyOneExitSlipPrLesson(otherExitSlips);
        }

        public static ExitSlip Create(string title, int maxQuestions, int lectureId, string appUserId, IEnumerable<ExitSlip> otherExitSlips)
        {
            return new ExitSlip(title, maxQuestions, appUserId, lectureId, otherExitSlips);
        }

        public void Publish(string appUserId)
        {
            EnsureExitSlipIsNotPublished();
            EnsureTeacherSameAsCreator(appUserId);
            IsPublished = true;
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

        public void CreateResponse(string text, string appUserId, int questionId)
        {
            EnsureExitSlipIsPublished();

            var question = GetQuestionById(questionId);
            question.AddResponse(text, appUserId);
        }

        public Response UpdateResponse(int responseId, string text, string appUserId, int questionId)
        {
            EnsureExitSlipIsPublished();

            var question = GetQuestionById(questionId);
            return question.UpdateResponse(responseId, text, appUserId);
        }

        public Response DeleteResponse(int responseId, string appUserId, int questionId)
        {
            var question = GetQuestionById(questionId);
            var response = question.DeleteResponse(responseId, appUserId);
            return response;
        }

        public void EnsureExitSlipIsNotPublished()
        {
            if (IsPublished)
                throw new InvalidOperationException("Cannot moditfy a published exitslip");
        }

        public void EnsureExitSlipIsPublished()
        {
            if (!IsPublished)
                throw new InvalidOperationException("Cannot add responses to an unpublished exitslip");
        }

        public void EnsureExitSlipDoesntExceedMaxQuestions()
        {
            if (Questions.Count >= MaxQuestions)
                throw new InvalidOperationException($"Cannot add more than {MaxQuestions} questions");
        }

        public void EnsureTeacherSameAsCreator(string appUserId)
        {
            if (!AppUserId.Equals(appUserId))
                throw new InvalidOperationException("Only the creator of the exit slip can modify it");
        }

        private Question GetQuestionById(int id)
        {
            var question = _questions.FirstOrDefault(q => q.Id == id);
            return question ?? throw new ArgumentException("Question was not found");
        }
    }
}
