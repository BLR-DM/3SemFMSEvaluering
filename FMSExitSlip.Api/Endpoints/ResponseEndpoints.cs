using System.Security.Claims;
using FMSExitSlip.Application.Commands.CommandDto.ResponseDto;
using FMSExitSlip.Application.Commands.Interfaces;

namespace FMSExitSlip.Api.Endpoints;

public static class ResponseEndpoints
{
    public static void MapResponseEndpoints(this WebApplication app)
    {
        const string tag = "Response";

        app.MapPost("/exitslip/{exitSlipId}/question/{questionId}/response",
            async (int exitSlipId, int questionId, CreateResponseDto responseDto, ClaimsPrincipal user,
                IExitSlipCommand command) =>
            {
                try
                {
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var role = user.FindFirst("usertype")?.Value;
                    await command.CreateResponseAsync(responseDto, exitSlipId, questionId, appUserId, role);
                    return Results.Created();
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            }).RequireAuthorization("student").WithTags(tag);

        app.MapPut("/exitslip/{exitSlipId}/question/{questionId}/response{responseId}",
            async (int exitSlipId, int responseId, int questionId, UpdateResponseDto responseDto, ClaimsPrincipal user,
                IExitSlipCommand command) =>
            {
                try
                {
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var role = user.FindFirst("usertype")?.Value;
                    await command.UpdateResponseAsync(responseDto, exitSlipId, responseId, questionId, appUserId, role);
                    return Results.Ok();
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            }).RequireAuthorization("student").WithTags(tag);
    }
}