using FMSEvaluering.Domain.Entities.PostEntities;
using FMSEvaluering.Domain.Values.DataServer;

namespace FMSEvaluering.Domain.Entities.ForumEntities;

public abstract class Forum : DomainEntity
{
    private readonly List<Post> _posts = [];

    protected Forum()
    {
    }

    public string Name { get; protected set; }
    public IReadOnlyCollection<Post> Posts => _posts;

    public virtual bool ValidateStudentAccessAsync(StudentValue student) => false;

    public virtual bool ValidateTeacherAccessAsync(TeacherValue teacher) => false;

    public void AddPost(string description, string solution, string appUserId)
    {
        var post = Post.Create(description, solution, appUserId);
        _posts.Add(post); 
    }

    public Post UpdatePost(int postId, string description, string solution, string appUserId)
    {
        var post = GetPostById(postId);
        post.Update(description, solution, appUserId);
        return post;
    }

    public Post DeletePost(int postId, string appUserId) //maaske slet? //admin ??
    {
        var post = GetPostById(postId);
        _posts.Remove(post);
        return post;
    }
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

    public Post GetPostById(int postId)
    {
        var post = Posts.SingleOrDefault(p => p.Id == postId);
        if (post is null) throw new ArgumentException("Post not found");
        return post;
    }

    public void ToReportData()
    {
        
    }
}