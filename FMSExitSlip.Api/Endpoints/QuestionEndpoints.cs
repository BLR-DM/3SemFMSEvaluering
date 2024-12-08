using System.Security.Claims;
using FMSExitSlip.Application.Commands.CommandDto.QuestionDto;
using FMSExitSlip.Application.Commands.Interfaces;

namespace FMSExitSlip.Api.Endpoints
{
    public static class QuestionEndpoints
    {
        public static void MapQuestionEndpoints(this WebApplication app)
        {
            const string tag = "Questions";

            app.MapPost("/exitslip/{id}/question",
                async (CreateQuestionDto questionDto, ClaimsPrincipal user, IExitSlipCommand command) =>
                {
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var role = user.FindFirst("usertype")?.Value;
                    try
                    {
                        await command.AddQuestionAsync(questionDto, appUserId, role);
                        return Results.Ok("Question added");
                    }
                    catch (Exception)
                    {
                        return Results.BadRequest("Failed to add the question");
                        throw;
                    }
                }).RequireAuthorization("Teacher").WithTags(tag);

            app.MapPut("/exitslip/{id}/question",
                async (int id, UpdateQuestionDto questionDto, ClaimsPrincipal user, IExitSlipCommand command) =>
                {
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (appUserId == null)
                        return Results.Unauthorized();

                    var role = user.FindFirst("usertype")?.Value;

                    try
                    {
                        await command.UpdateQuestionAsync(questionDto, id, appUserId, role);
                        return Results.Ok("Question updated");
                    }
                    catch (Exception)
                    {
                        return Results.BadRequest("Couldn't update the question");
                    }


                }).RequireAuthorization("Teacher").WithTags(tag);
        }
    }
}
