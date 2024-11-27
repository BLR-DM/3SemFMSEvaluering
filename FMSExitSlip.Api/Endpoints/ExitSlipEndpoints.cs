using System.Security.Claims;
using FMSExitSlip.Application.Commands.CommandDto.ExitSlipDto;
using FMSExitSlip.Application.Commands.Interfaces;

namespace FMSExitSlip.Api.Endpoints
{
    public static class ExitSlipEndpoints
    {
        public static void MapExitSlipEndpoints(this WebApplication app)
        {
            app.MapPost("/exitslip", async (CreateExitSlipDto exitslipDto, HttpContext httpContext, IExitSlipCommand command) =>
            {
                var appUserId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                try
                {
                    await command.CreateExitSlipAsync(exitslipDto, appUserId);
                    return Results.Ok("Exitslip created");
                }
                catch (Exception)
                {

                    return Results.BadRequest("Failed to create ExitSlip");
                }
            }).RequireAuthorization("Teacher").WithTags("ExitSlip");

            app.MapPut("/exitslip/{id}/publish", async (int id, HttpContext HttpContext, IExitSlipCommand command) =>
            {
                var appUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    await command.PublishExitSlip(id, appUserId);
                    return Results.Ok("ExitSlip published");
                }
                catch (Exception)
                {

                    return Results.BadRequest("Failed to publish ExitSlip");
                }

            }).RequireAuthorization("Teacher").WithTags("ExitSlip");
        }
    }
}
