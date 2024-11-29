﻿using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Domain.Entities.ForumEntities
{
    public abstract class Forum : DomainEntity
    {
        private readonly List<Post> _posts = [];

        protected Forum()
        {
        }

        public string Name { get; protected set; }
        public IReadOnlyCollection<Post> Posts => _posts;

        public virtual void ValidatePostCreation(string studentId)
        {
        }

        //public void AddPost(Post post)
        //{
        //    _posts.Add(post);
        //}

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
