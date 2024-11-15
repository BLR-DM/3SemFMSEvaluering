using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Commands.CommandDto.CommentDto
{
    public record UpdateCommentDto(int commentID, string text, byte[] rowVersion, int postID)
    {
    }
}
