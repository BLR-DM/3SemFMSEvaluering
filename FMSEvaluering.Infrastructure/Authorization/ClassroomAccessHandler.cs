﻿using FMSEvaluering.Application.Authorization;
using FMSEvaluering.Infrastructure.ExternalServices;
using Microsoft.AspNetCore.Authorization;

namespace FMSEvaluering.Infrastructure.Authorization;

public class ClassroomAccessHandler : AuthorizationHandler<ClassroomAccessRequirement>
{
    private readonly FmsProxy _fmsProxy;

    public ClassroomAccessHandler(FmsProxy fmsProxy)
    {
        _fmsProxy = fmsProxy;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClassroomAccessRequirement requirement)
    {
        // Check if the user has a claim for "StudentId"
        var studentId = context.User.FindFirst("sub").Value;
        if (string.IsNullOrEmpty(studentId))
        {
            return Task.CompletedTask;
        }

        var classId = _fmsProxy.StudentIsPartOfClassroom(studentId);

        if (!classId.Result.Equals(requirement.ClassroomId))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}