﻿using System.Security.Claims;
using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.Interfaces;

namespace FMSEvaluering.Api.Endpoints
{
    public static class CommentEndpoints
    {
        public static void MapCommentEndpoints(this WebApplication app)
        {
            const string tag = "Comment";


            app.MapPost("/forum/{forumId}/post/{postId}/comment", async (CreateCommentDto dto, int forumId, int postId, ClaimsPrincipal user, IPostCommand command) =>
            {
                try
                {
                    var appUserId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                    var role = user.FindFirstValue("usertype")!;
                    var firstName = user.FindFirstValue(ClaimTypes.GivenName)!;
                    var lastName = user.FindFirstValue(ClaimTypes.Surname)!;

                    await command.CreateCommentAsync(dto, firstName, lastName, postId, appUserId, role, forumId);
                    return Results.Created("testURI", dto);
                }
                catch (Exception)
                {
                    return Results.BadRequest("Couldn't create comment");
                }
            }).RequireAuthorization("student").WithTags(tag);
                

            app.MapPut("/forum/{forumId}/post/{postId}/comment/{commentId}", async (UpdateCommentDto dto, int forumId, int postId,
                int commentId, ClaimsPrincipal user, IPostCommand command) =>
            {
                try
                {
                    var appUserId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                    var role = user.FindFirstValue("usertype")!;

                    await command.UpdateCommentAsync(dto, appUserId, role, forumId, postId, commentId);
                    return Results.Ok(dto);
                }
                catch (Exception)
                {
                    return Results.BadRequest("Couldn't update comment");
                }
            }).RequireAuthorization("student").WithTags(tag);
        }
    }
}
