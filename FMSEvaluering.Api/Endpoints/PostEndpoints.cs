using System.Security.Claims;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Queries.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FMSEvaluering.Api.Endpoints;

public static class PostEndpoints
{
    public static void MapPostEndpoints(this WebApplication app)
    {
        const string tag = "Post";

        app.MapPost("/forum/{forumId}/post",
            async (int forumId, CreatePostDto post, ClaimsPrincipal user, IForumCommand command) =>
            {
                try
                {
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var role = user.FindFirst("usertype")?.Value;
                    await command.CreatePostAsync(post, appUserId, role, forumId);
                    return Results.Created("testURI", post); // Test return value
                }
                catch (Exception)
                {
                    return Results.Problem("Couldn't create post");
                }
            }).RequireAuthorization("student").WithTags(tag);

        app.MapPut("/forum/{forumId}/post/{postId}",
            async (int forumId, int postId, UpdatePostDto post, ClaimsPrincipal user, IForumCommand command) =>
            {
                try
                {
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var role = user.FindFirst("usertype")?.Value;
                    await command.UpdatePostAsync(post, appUserId, role, postId, forumId);
                    return Results.Created("testURI", post);
                }
                catch (Exception)
                {
                    return Results.Problem("Couldn't update post", statusCode: 500); // test
                }
            }).RequireAuthorization("student").WithTags(tag);

        app.MapDelete("/forum/{forumId}/post/{postId}",
            async (int forumId, int postId, [FromBody] DeletePostDto post, ClaimsPrincipal user,
                IForumCommand command) =>
            {
                var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = user.FindFirst("usertype")?.Value;
                await command.DeletePostAsync(post, appUserId, role, postId, forumId);
                return Results.Ok("Post deleted");
            }).WithTags(tag); //admin?

        app.MapGet("/forum/{forumId}/post/{postId}",
            async (int forumId, int postId, ClaimsPrincipal user, IForumQuery query) =>
            {
                try
                {
                    var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var role = user.FindFirst("usertype")?.Value;
                    var post = await query.GetForumWithSinglePostAsync(forumId, appUserId, role, postId);
                    return Results.Ok(post);
                }
                catch (Exception)
                {
                    return Results.Problem("Couldn't get post");
                }
            }).WithTags(tag);
    }
}