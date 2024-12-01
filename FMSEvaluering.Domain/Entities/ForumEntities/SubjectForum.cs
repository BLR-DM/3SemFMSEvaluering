namespace FMSEvaluering.Domain.Entities.ForumEntities
{
    public class SubjectForum : Forum
    {
        public int SubjectId { get; set; }

        internal SubjectForum(string name, int subjectId)
        {
            Name = name;
            SubjectId = subjectId;
        }

        public override async Task<bool> ValidateUserAccessToForum(string studentData, IServiceProvider serviceProvider, string role)
        {
            // Do something
            return false;
        }
    }
}
