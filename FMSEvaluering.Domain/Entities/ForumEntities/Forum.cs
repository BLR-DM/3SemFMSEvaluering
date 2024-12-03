using FMSEvaluering.Domain.Entities.PostEntities;

namespace FMSEvaluering.Domain.Entities.ForumEntities;

public abstract class Forum : DomainEntity
{
    private readonly List<Post> _posts = [];

    public string Name { get; protected set; }
    public IReadOnlyCollection<Post> Posts => _posts;

    public virtual Task<bool> ValidateUserAccessAsync(string userId, IServiceProvider serviceProvider,
        string role)
    {
        return Task.FromResult(false);
    }

    public async Task AddPostAsync(string description, string solution, string appUserId,
        IServiceProvider serviceProvider, string role)
    {
        await ValidateAccessAsync(appUserId, serviceProvider, role);

        var post = Post.Create(description, solution, appUserId);
        _posts.Add(post);
    }

    public async Task<Post> UpdatePostAsync(int postId, string description, string solution, string appUserId,
        IServiceProvider serviceProvider, string role)
    {
        await ValidateAccessAsync(appUserId, serviceProvider, role);

        var post = GetPostById(postId);
        post.Update(description, solution, appUserId);
        return post;
    }

    public async Task<Post> DeletePostAsync(int postId, string appUserId, IServiceProvider serviceProvider, string role)
    {
        await ValidateAccessAsync(appUserId, serviceProvider, role);

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


    private async Task ValidateAccessAsync(string appUserId, IServiceProvider serviceProvider, string role)
    {
        var hasAccess = await ValidateUserAccessAsync(appUserId, serviceProvider, role);
        if (!hasAccess) throw new ArgumentException("Not authorized");
    }

    private Post GetPostById(int postId)
    {
        var post = Posts.SingleOrDefault(p => p.Id == postId);
        if (post is null) throw new ArgumentException("Post not found");
        return post;
    }
}