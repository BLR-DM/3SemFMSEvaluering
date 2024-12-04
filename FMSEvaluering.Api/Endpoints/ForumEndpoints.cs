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
                await command.CreatePublicForumAsync(createPublicForumDto);
            }).WithTags(tag);

            app.MapPost("/forum/class", async (CreateClassForumDto createClassForumDto, IForumCommand command) =>
            {
                await command.CreateClassForumAsync(createClassForumDto);
            }).WithTags(tag);

            app.MapPost("/forum/subject", async (CreateSubjectForumDto createSubjectForumDto, IForumCommand command) =>
            {
                await command.CreateSubjectForumAsync(createSubjectForumDto);
            }).WithTags(tag);

            app.MapDelete("/forum", async ([FromBody]DeleteForumDto deleteForumDto, IForumCommand command) =>
            {
                await command.DeleteForumAsync(deleteForumDto);
            }).WithTags(tag);

            app.MapGet("/forum", async (IForumQuery query, ClaimsPrincipal user) =>
            {
                var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = user.FindFirst("usertype")?.Value;

                return await query.GetForumsAsync(appUserId, role);
            }).WithTags(tag);

            app.MapGet("/forum/{id}", async (int id, IForumQuery query) =>
            {
                return await query.GetForumAsync(id);
            }).WithTags(tag);

            // Oversigt over Posts for given forum (includer ikke history, comments)
            //app.MapGet("/forum/{id}/post", async (int id, IForumQuery query) =>
            //{
            //    var result = await query.GetForumWithPostsAsync(id);
            //    return Results.Ok(result);
            //}).WithTags(tag);

            // hent forum for en teacher med posts med votes over 2
            app.MapGet("/forum/{id}/posts/teacher", async (int id, ClaimsPrincipal user, IForumQuery query) =>
            {
                var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = user.FindFirst("usertype")?.Value;

                var result = await query.GetForumWithPostsForTeacherAsync(id, appUserId, role, 2);
                return Results.Ok(result);
            }).WithTags(tag).RequireAuthorization("Teacher");

        }
    }
}
