namespace FMSEvaluering.Domain.Entities.ForumEntities
{
    public class ClassForum : Forum
    {
        public int ClassId { get; set; }

        internal ClassForum(string name, int classId)
        {
            Name = name;
            ClassId = classId;
        }

        public override void ValidatePostCreation(string studentClassId)
        {
            if (!ClassId.ToString().Equals(studentClassId))
                throw new InvalidOperationException("Student is not part of class.");
        }
    }
}
