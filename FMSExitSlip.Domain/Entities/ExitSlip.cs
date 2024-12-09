using FMSExitSlip.Domain.Values.DataServer;

namespace FMSExitSlip.Domain.Entities;

public class ExitSlip : DomainEntity
{
    protected readonly List<Question> _questions = [];

    protected ExitSlip()
    {
    }

    private ExitSlip(string title, int maxQuestions, int lectureId,
        IEnumerable<ExitSlip> otherExitSlips)
    {
        Title = title;
        MaxQuestions = maxQuestions;
        LectureId = lectureId;

        AssureOnlyOneExitSlipPrLesson(otherExitSlips);
    }

    public string Title { get; protected set; }
    public int MaxQuestions { get; protected set; }
    public bool IsPublished { get; protected set; }
    public int LectureId { get; protected set; }

    public ICollection<Question> Questions => _questions;

    public static ExitSlip Create(string title, int maxQuestions, int lectureId,
        IEnumerable<ExitSlip> otherExitSlips)
    {
        return new ExitSlip(title, maxQuestions, lectureId, otherExitSlips);
    }


    public void Publish(string appUserId)
    {
        EnsureExitSlipIsNotPublished();
        EnsureQuestionsBeforePublish();
        //EnsureTeacherSameAsCreator(appUserId);
        IsPublished = true;
    }

    protected void AssureOnlyOneExitSlipPrLesson(IEnumerable<ExitSlip> otherExitSlips)
    {
        if (otherExitSlips.Any(l => l.LectureId == LectureId))
            throw new Exception("Only one exit slip per lesson is allowed.");
    }

    public void CreateQuestion(string text, string appUserId)
    {
        //EnsureTeacherSameAsCreator(appUserId);
        EnsureExitSlipDoesntExceedMaxQuestions();
        EnsureExitSlipIsNotPublished();

        var question = Question.Create(text, appUserId);
        _questions.Add(question);
    }

    public Question UpdateQuestion(int questionId, string text, string appUserId)
    {
        //EnsureTeacherSameAsCreator(appUserId);
        EnsureExitSlipIsNotPublished();

        var question = GetQuestionById(questionId);
        question.Update(text);
        return question;
    }

    public Question DeleteQuestion(int questionId, string appUserId)
    {
        //EnsureTeacherSameAsCreator(appUserId);

        var question = GetQuestionById(questionId);
        _questions.Remove(question);
        return question;
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

    public void EnsureQuestionsBeforePublish()
    {
        if (Questions.Count == 0)
            throw new InvalidOperationException("Cannot publish an exit slip without questions");
    }

    public void EnsureExitSlipDoesntExceedMaxQuestions()
    {
        if (Questions.Count >= MaxQuestions)
            throw new InvalidOperationException($"Cannot add more than {MaxQuestions} questions");
    }

    //public void EnsureTeacherSameAsCreator(string appUserId)
    //{
    //    if (!AppUserId.Equals(appUserId))
    //        throw new InvalidOperationException("Only the creator of the exit slip can modify it");
    //}

    private Question GetQuestionById(int id)
    {
        var question = Questions.FirstOrDefault(q => q.Id == id);
        return question ?? throw new ArgumentException("Question not found");
    }

    public void ValidateStudentAccess(StudentValue student)
    {
        var hasAccess =
            student.Class.TeacherSubjects.Any(ts => ts.Lectures.Any(l => l.Id.Equals(LectureId.ToString())));
        if (!hasAccess)
            throw new InvalidOperationException("Only students in the lecture can access the exit slip");
    }

    public void ValidateTeacherAccess(TeacherValue teacher)
    {
        var hasAccess = teacher.TeacherSubjects.Any(ts => ts.Lectures.Any(l => l.Id.Equals(LectureId.ToString())));
        if (!hasAccess)
            throw new InvalidOperationException("Only the teacher who has the lecture, can create an exitslip");
    }
}