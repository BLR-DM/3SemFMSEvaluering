﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Queries.QueryDto
{
    public record ForumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ForumType { get; set; }
        public byte[] RowVersion { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }

    }

    public record PublicForumDto : ForumDto
    {
    }
    public record ClassForumDto : ForumDto
    {
        public int ClassId { get; set; }
    }
    public record SubjectForumDto : ForumDto
    {
        public int SubjectId { get; set; }
    }
}
