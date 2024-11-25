using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.Commands.CommandDto.ForumDto;

namespace FMSEvaluering.Application.Commands.Interfaces
{
    public interface IForumCommand
    {
        Task CreatePublicForumAsync(CreatePublicForumDto forumDto);
        Task CreateClassForumAsync(CreateClassForumDto forumDto);
        Task CreateSubjectForumAsync(CreateSubjectForumDto forumDto);

        Task DeleteForumAsync(DeleteForumDto forumDto);
    }
}
