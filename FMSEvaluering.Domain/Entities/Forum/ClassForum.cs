using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Domain.Entities.Forum
{
    public class ClassForum : Forum
    {
        public int ClassId { get; set; }

        internal ClassForum(string name, int classId)
        {
            Name = name;
            ClassId = classId;
        }
    }
}
