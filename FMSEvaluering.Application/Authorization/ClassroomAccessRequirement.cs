using Microsoft.AspNetCore.Authorization;

namespace FMSEvaluering.Application.Authorization;

public class ClassroomAccessRequirement : IAuthorizationRequirement
{
    public ClassroomAccessRequirement(string classroomId)
    {
        ClassroomId = classroomId;
    }

    public string ClassroomId { get; }
}