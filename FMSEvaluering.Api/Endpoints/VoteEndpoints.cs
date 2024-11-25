using System.Security.Claims;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FMSEvaluering.Api.Endpoints
{
    public static class VoteEndpoints
    {

        public static void MapVoteEndpoints(this WebApplication app)
        {
            //app.MapPost("/post/vote", async (CreateVoteDto dto, IPostCommand command) =>
            //{
            //    await command.CreateVote(dto);
            //}).WithTags("Vote");
            //app.MapPut("/post/vote", async (UpdateVoteDto dto, IPostCommand command) =>
            //{
            //    await command.UpdateVote(dto);
            //}).WithTags("Vote");

            //app.MapDelete("/post/vote", async ([FromBody] DeleteVoteDto dto, IPostCommand command) =>
            //{
            //    await command.DeleteVote(dto);
            //}).WithTags("Vote");


            //app.MapPost("/post/{postId}/vote",
            //    async (int postId, CreateVoteDto voteDto, HttpContext httpContext, IPostCommand command) =>
            //    {
            //        var appUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //        try
            //        {
            //            await command.HandleVote(voteDto, appUserId, postId);
            //            return Results.Ok("Vote registered");
            //        }
            //        catch (Exception)
            //        {
            //            return Results.BadRequest("Failed to register the vote");
            //            throw;
            //        }


            //    }).WithTags(("Vote"));
        }
    }
}
