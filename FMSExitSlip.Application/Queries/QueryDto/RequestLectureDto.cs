namespace FMSExitSlip.Application.Queries.QueryDto;

public record RequestLectureDto
{
    public int ClassId { get; set; }
    public int SubjectId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string TeacherAppUserId { get; set; }
};