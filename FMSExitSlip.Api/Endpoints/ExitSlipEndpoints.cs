using System.Security.Claims;
using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.Interfaces;
using FMSExitSlip.Application.Queries.Interfaces;
using FMSExitSlip.Application.Services;

namespace FMSExitSlip.Api.Endpoints;

public static class ExitSlipEndpoints
{
    public static void MapExitSlipEndpoints(this WebApplication app)
    {
        const string tag = "Exitslip";

        app.MapPost("/exitslip",
            async (HttpContext httpContext, IExitSlipGenerator exitSlipGenerator, IExitSlipCommand command) =>
            {
                try
                {
                    await exitSlipGenerator.GenerateExitslipsForLectures();
                    return Results.Created();
                }
                catch (Exception)
                {
                    return Results.BadRequest("Failed to create ExitSlip");
                }
            }).RequireAuthorization("teacher").WithTags(tag);

        app.MapPut("/exitslip/{id}/publish", async (int id, PublishExitSlipDto publishExitSlipDto,
            ClaimsPrincipal user, IExitSlipCommand command) =>
        {
            try
            {
                var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = user.FindFirst("usertype")?.Value;
                await command.PublishExitSlip(id, appUserId!, publishExitSlipDto, role!);
                return Results.Ok("ExitSlip published");
            }
            catch (Exception)
            {
                return Results.BadRequest("Failed to publish ExitSlip");
            }
        }).RequireAuthorization("teacher").WithTags(tag);

        app.MapGet("/exitslip/{id}", async (int id, IExitSlipQuery query, ClaimsPrincipal user) =>
        {
            try
            {
                var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = user.FindFirst("usertype")?.Value;
                var result = await query.GetExitSlipAsync(id, appUserId, role);
                return Results.Ok(result);
            }
            catch (Exception)
            {
                return Results.BadRequest();
            }
        }).WithTags(tag);
    }
}