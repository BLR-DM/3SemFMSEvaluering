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
            app.MapPost("/forum/public", async (CreatePublicForumDto createPublicForumDto, IForumCommand command) =>
            {
                await command.CreatePublicForumAsync(createPublicForumDto);
            }).WithTags("Forum");

            app.MapPost("/forum/class", async (CreateClassForumDto createClassForumDto, IForumCommand command) =>
            {
                await command.CreateClassForumAsync(createClassForumDto);
            }).WithTags("Forum");

            app.MapPost("/forum/subject", async (CreateSubjectForumDto createSubjectForumDto, IForumCommand command) =>
            {
                await command.CreateSubjectForumAsync(createSubjectForumDto);
            }).WithTags("Forum");

            app.MapDelete("/forum", async ([FromBody]DeleteForumDto deleteForumDto, IForumCommand command) =>
            {
                await command.DeleteForumAsync(deleteForumDto);
            }).WithTags("Forum");

            app.MapGet("/forum", async (IForumQuery query) =>
            {
                return await query.GetForumsAsync();
            }).WithTags("Forum");

            app.MapGet("/forum/{id}", async (int id, IForumQuery query) =>
            {
                return await query.GetForumAsync(id);
            }).WithTags("Forum");

            // Oversigt over Posts for given forum (includer ikke history, comments)
            //app.MapGet("/forum/{id}/post", async (int id, IForumQuery query) =>
            //{
            //    var result = await query.GetForumWithPostAsync(id);
            //    return Results.Ok(result);
            //}).WithTags("Forum");

        }
    }
}
