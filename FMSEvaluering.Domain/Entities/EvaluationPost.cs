using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Domain.Entities
{
    public class EvaluationPost : DomainEntity
    {
        public string Description { get; protected set; } 
        protected EvaluationPost() {}

        private EvaluationPost(string description)
        {
            Description = description;
        }

        public static EvaluationPost Create(string description)
        {
            return new EvaluationPost(description);
        }
    }
}
