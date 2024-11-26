using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Domain.Entities
{
    public class Response : DomainEntity
    {
        public string Text { get; protected set; }
        public string AppUserId { get; protected set; }
    }
}
