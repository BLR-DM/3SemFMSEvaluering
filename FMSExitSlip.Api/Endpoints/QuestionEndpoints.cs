using System.Security.Claims;
using FMSExitSlip.Application.Commands.CommandDto.QuestionDto;
using FMSExitSlip.Application.Commands.Interfaces;

namespace FMSExitSlip.Api.Endpoints;

public static class QuestionEndpoints
{
    public static void MapQuestionEndpoints(this WebApplication app)
    {
        const string tag = "Questions";

        app.MapPost("/exitslip/{exitSlipId}/question",
            async (int exitSlipId, CreateQuestionDto questionDto, ClaimsPrincipal user, IExitSlipCommand command) =>
            {
                var appUserId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var role = user.FindFirstValue("usertype")!;
                try
                {
                    await command.AddQuestionAsync(questionDto, exitSlipId, appUserId, role);
                    return Results.Created();
                }
                catch (Exception)
                {
                    return Results.BadRequest("Failed to add the question");
                }
            }).RequireAuthorization("teacher").WithTags(tag);

        app.MapPut("/exitslip/{exitSlipId}/question/{questionId}",
            async (int exitSlipId, int questionId, UpdateQuestionDto questionDto, ClaimsPrincipal user, IExitSlipCommand command) =>
            {
                var appUserId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;

                if (appUserId == null)
                    return Results.Unauthorized();

                var role = user.FindFirstValue("usertype")!;

                try
                {
                    await command.UpdateQuestionAsync(questionDto, exitSlipId, questionId, appUserId, role);
                    return Results.Ok("Question updated");
                }
                catch (Exception)
                {
                    return Results.BadRequest("Couldn't update the question");
                }
            }).RequireAuthorization("teacher").WithTags(tag);
    }
}