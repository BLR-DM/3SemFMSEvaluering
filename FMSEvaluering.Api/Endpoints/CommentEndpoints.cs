using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.Interfaces;

namespace FMSEvaluering.Api.Endpoints
{
    public static class CommentEndpoints
    {
        public static void MapCommentEndpoints(this WebApplication app)
        {
            const string tag = "Comment";


            app.MapPost("/forum/{id}/post/{id}/comment",
                async (CreateCommentDto dto, IPostCommand command) =>
                    await command.CreateCommentAsync(dto)).WithTags(tag);

            app.MapPut("/forum/{id}/post/{id}/comment",
                async (UpdateCommentDto dto, IPostCommand command) =>
                    await command.UpdateCommentAsync(dto)).WithTags(tag);
        }
    }
}
