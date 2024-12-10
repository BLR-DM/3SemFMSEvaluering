using System.Data;
using System.Security.Claims;
using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.Interfaces;
using FMSExitSlip.Application.Queries.Interfaces;
using FMSExitSlip.Application.Queries.QueryDto;
using FMSExitSlip.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FMSExitSlip.Api.Endpoints
{
    public static class ExitSlipEndpoints
    {
        public static void MapExitSlipEndpoints(this WebApplication app)
        {
            app.MapPost("/exitslip", async (HttpContext httpContext, IExitSlipGenerator exitSlipGenerator, IExitSlipCommand command) =>
            {
                try
                {
                    await exitSlipGenerator.GenerateExitslipsForLectures();
                    return Results.Ok("Exitslip created");
                }
                catch (Exception)
                {

                    return Results.BadRequest("Failed to create ExitSlip");
                }
            }).RequireAuthorization("Teacher").WithTags("ExitSlip");

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

            }).RequireAuthorization("Teacher").WithTags("ExitSlip");

            app.MapGet("/exitslip/{id}", async (int id, IExitSlipQuery query, ClaimsPrincipal user) =>
            {
                var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = user.FindFirst("usertype")?.Value;

                var result = await query.GetExitSlipAsync(id, appUserId, role);
                return Results.Ok(result);
            });

            app.MapGet("/teacher/exitslip",
                async ([FromBody] RequestLectureDto requestDto, IExitSlipQuery query, ClaimsPrincipal user) =>
                {
                    try
                    {
                        var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        var role = user.FindFirst("usertype")?.Value;

                        //requestDto.TeacherAppUserId = appUserId!;
                        //requestDto.StartDate = new DateTime(2024, 11, 01);
                        //requestDto.EndDate = new DateTime(2024, 12, 08);

                        var result = await query.GetExitSlipsAsync(requestDto, role!);
                        return Results.Ok(result);
                    }
                    catch (Exception)
                    {
                        return Results.BadRequest("Failed to fetch exitslips");
                        throw;
                    }
                });
        }
    }
}
