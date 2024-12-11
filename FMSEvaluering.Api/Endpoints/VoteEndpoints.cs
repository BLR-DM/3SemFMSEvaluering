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
            const string tag = "Vote";

            app.MapPost("/forum/{forumId}/post/{postId}/vote",
                async (int forumId, int postId, HandleVoteDto voteDto, ClaimsPrincipal user, IPostCommand command) =>
                {
                    // var id = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var role = user.FindFirst("usertype")?.Value;

                    try
                    {
                        await command.HandleVote(voteDto, appUserId, role, forumId, postId);
                        return Results.Ok("Vote registered");
                    }
                    catch (Exception)
                    {
                        return Results.BadRequest("Failed to register the vote");
                    }
                }).RequireAuthorization("student").WithTags(tag);
        }
    }
}
