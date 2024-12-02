using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Application.Queries.QueryDto
{
    public record PostHistoryDto
    {
        public string Description { get; set; }
        public string Solution { get; set; }
        public string EditedDate { get; set; }
    }
}
