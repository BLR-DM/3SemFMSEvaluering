﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Queries.QueryDto
{
    public record CommentDto
    {
        public int Id { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Text { get; set; }
        public string CreatedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public string AppUserId { get; set; }
    }
}
