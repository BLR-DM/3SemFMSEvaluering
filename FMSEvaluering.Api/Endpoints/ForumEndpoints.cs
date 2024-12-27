using System.Security.Claims;
using FMSEvaluering.Application.Commands.CommandDto.ForumDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Queries.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FMSEvaluering.Api.Endpoints
{
    public static class ForumEndpoints
    {
        public static void MapForumEndpoints(this WebApplication app)
        {
            const string tag = "Forum";

            app.MapPost("/forum/public", async (CreatePublicForumDto createPublicForumDto, IForumCommand command) =>
            {
                try
                {
                    await command.CreatePublicForumAsync(createPublicForumDto);
                    return Results.Created();
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            }).WithTags(tag);

            app.MapPost("/forum/class", async (CreateClassForumDto createClassForumDto, IForumCommand command) =>
            {
                try
                {
                    await command.CreateClassForumAsync(createClassForumDto);
                    return Results.Created();
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            }).WithTags(tag);

            app.MapPost("/forum/subject", async (CreateSubjectForumDto createSubjectForumDto, IForumCommand command) =>
            {
                try
                {
                    await command.CreateSubjectForumAsync(createSubjectForumDto);
                    return Results.Created();
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            }).WithTags(tag);

            app.MapDelete("/forum/{forumId}", async (int forumId, [FromBody]DeleteForumDto deleteForumDto, IForumCommand command) =>
            {
                try
                {
                    await command.DeleteForumAsync(deleteForumDto, forumId);
                    return Results.Ok();
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            }).WithTags(tag);

            app.MapGet("/forum", async (IForumQuery query, ClaimsPrincipal user) =>
            {
                try
                {
                    var appUserId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                    var role = user.FindFirstValue("usertype")!;
                    var result = await query.GetForumsAsync(appUserId, role);
                    return Results.Ok(result);
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            }).WithTags(tag);

            app.MapGet("/forum/{forumId}", async (int forumId, IForumQuery query) => // <- Skal slettes?
            {
                try
                {
                    var result = await query.GetForumAsync(forumId);
                    return Results.Ok(result);
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            }).RequireAuthorization("student").WithTags(tag);

            // hent forum for en teacher med posts med votes over 2
            app.MapGet("/forum/{forumId}/posts/teacher", async (int forumId, ClaimsPrincipal user, IForumQuery query) => 
            {
                try
                {
                    var appUserId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                    var role = user.FindFirstValue("usertype")!;
                    var result = await query.GetForumWithPostsForTeacherAsync(forumId, appUserId, role, 2);
                    return Results.Ok(result);
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            }).RequireAuthorization("teacher").WithTags(tag); //check igennem

            app.MapGet("/forum/{forumId}/posts",
                async (int forumId, ClaimsPrincipal user, IForumQuery query) =>
                {
                    try
                    {
                        var appUserId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                        var role = user.FindFirstValue("usertype")!;
                        var posts = await query.GetForumWithPostsAsync(forumId, appUserId, role);
                        return Results.Ok(posts);
                    }
                    catch (Exception)
                    {
                        return Results.BadRequest("Couldn't get posts");
                    }
                }).RequireAuthorization("student").WithTags(tag);

        }
    }
}
