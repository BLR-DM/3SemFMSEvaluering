using System.Security.Claims;
using FMSExitSlip.Application.Commands.CommandDto.ResponseDto;
using FMSExitSlip.Application.Commands.Interfaces;

namespace FMSExitSlip.Api.Endpoints
{
    public static class ResponseEndpoints
    {
        public static void MapResponseEndpoints(this WebApplication app)
        {
            const string tag = "Response";

            app.MapPost("/exitslip/{id}/question/response",
                async (int id, CreateResponseDto responseDto, ClaimsPrincipal user, IExitSlipCommand command) =>
                {
                    //var appUserId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var role = user.FindFirst("usertype")?.Value;
                    await command.CreateResponseAsync(responseDto, id, role);
                }).RequireAuthorization("Student").WithTags(tag);

            app.MapPut("/exitslip/{id}/question/response",
                async (int id, UpdateResponseDto responseDto, ClaimsPrincipal user, IExitSlipCommand command) =>
                {
                    //var appUserId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var role = user.FindFirst("usertype")?.Value;
                    await command.UpdateResponseAsync(responseDto, id, role);
                }).RequireAuthorization("Student").WithTags(tag);
        }
    }
}
