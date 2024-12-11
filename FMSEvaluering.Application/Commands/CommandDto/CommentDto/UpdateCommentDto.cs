namespace FMSEvaluering.Application.Commands.CommandDto.CommentDto
{
    public record UpdateCommentDto(string Text, byte[] RowVersion);
}