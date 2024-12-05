using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.Interfaces;

namespace FMSEvaluering.Api.Endpoints
{
    public static class CommentEndpoints
    {
        public static void MapCommentEndpoints(this WebApplication app)
        {
            const string tag = "Comment";


            app.MapPost("/forum/{forumId}/post/{postId}/comment",
                async (CreateCommentDto dto, IPostCommand command) =>
                    await command.CreateCommentAsync(dto)).WithTags(tag);

            app.MapPut("/forum/{forumId}/post/{postId}/comment",
                async (UpdateCommentDto dto, IPostCommand command) =>
                    await command.UpdateCommentAsync(dto)).WithTags(tag);
        }
    }
}
