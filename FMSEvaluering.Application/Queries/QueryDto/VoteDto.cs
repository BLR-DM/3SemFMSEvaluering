﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Queries.QueryDto
{
    public record VoteDto
    {
        public bool VoteType { get; set; }
        public byte[] RowVersion { get; set; }
        public string AppUserId { get; set; }
    }
}
