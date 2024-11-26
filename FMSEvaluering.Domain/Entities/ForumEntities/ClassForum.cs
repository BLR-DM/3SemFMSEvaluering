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

        public override void ValidatePostCreation(string studentId) // FmsProxy?
        {
            if (!studentId.Equals(ClassId.ToString())) // studentId = user.ClassId eller ClassId
                throw new InvalidOperationException("Student is not part of class.");
        }
    }
}
