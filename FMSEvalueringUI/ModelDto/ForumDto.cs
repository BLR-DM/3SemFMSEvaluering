namespace FMSEvalueringUI.ModelDto
{
    public class ForumDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ForumType { get; set; }
    }

    public class PublicForumDto : ForumDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ForumType { get; set; }
    }

    public class ClassForumDto : ForumDto
    {
        public string ClassId { get; set; }
    }

    public class SubjectForumDto : ForumDto
    {
        public string SubjectId { get; set; }
    }
}
