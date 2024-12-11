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

            app.MapPost("/exitslip/{exitSlipId}/question/response",
                async (int exitSlipId, CreateResponseDto responseDto, ClaimsPrincipal user, IExitSlipCommand command) =>
                {
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var role = user.FindFirst("usertype")?.Value;
                    await command.CreateResponseAsync(responseDto, exitSlipId, appUserId, role);
                    
                }).RequireAuthorization("student").WithTags(tag);

            app.MapPut("/exitslip/{exitSlipId}/question/response",
                async (int exitSlipId, UpdateResponseDto responseDto, ClaimsPrincipal user, IExitSlipCommand command) =>
                {
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var role = user.FindFirst("usertype")?.Value;
                    await command.UpdateResponseAsync(responseDto, exitSlipId, appUserId, role);
                }).RequireAuthorization("student").WithTags(tag);
        }
    }
}
