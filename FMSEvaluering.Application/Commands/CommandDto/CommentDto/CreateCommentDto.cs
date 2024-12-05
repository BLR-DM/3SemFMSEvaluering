using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Commands.CommandDto.CommentDto
{
    public record CreateCommentDto(string firstName, string lastName, string Text);
}
