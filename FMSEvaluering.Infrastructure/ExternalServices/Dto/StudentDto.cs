namespace FMSEvaluering.Infrastructure.ExternalServices.Dto;

public record StudentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ClassId { get; set; }
    public string AppUserId { get; set; }
}