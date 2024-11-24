using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Domain.Entities.Forum
{
    public class PublicForum : Forum
    {
        internal PublicForum(string name)
        {
            Name = name;
        }
    }
}
