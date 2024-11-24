using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Domain.Entities.Forum
{
    public abstract class Forum : DomainEntity
    {
        public string Name { get; protected set; }
        private readonly List<Post> _posts = new List<Post>();

        protected Forum() { }

        public IReadOnlyCollection<Post> Posts => _posts;



        public static Forum CreatePublicForum(string name)
        {
            return new PublicForum(name);
        }

        public static Forum CreateClassForum(string name, int classId)
        {
            return new ClassForum(name, classId);
        }

        public static Forum CreateSubjectForum(string name, int subjectId)
        {
            return new SubjectForum(name, subjectId);
        }

    }
}
