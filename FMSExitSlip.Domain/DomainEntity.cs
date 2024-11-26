using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Domain
{
    public abstract class DomainEntity
    {
        public int Id { get; protected set; }
        [Timestamp]
        public byte[] RowVersion { get; protected set; } = null!;
    }
}
