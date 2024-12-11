using System.Text;
using FMSEvaluering.Application.Helpers;
using FMSEvaluering.Application.Queries.QueryDto;

namespace FMSEvaluering.Infrastructure.Helpers;

public class CsvGenerator : ICsvGenerator
{
    async Task<Stream> ICsvGenerator.GenerateCsvForPosts(ForumDto forum)
    {
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream, Encoding.UTF8);

        writer.WriteLine($"Forum: {forum.Name}");
        writer.WriteLine($"-----------------------------------------------");
        foreach (var post in forum.Posts)
        {
            writer.WriteLine($"Date: {post.CreatedDate}");
            writer.WriteLine($"Beskrivelse: {post.Description}");
            writer.WriteLine($"Løsning: {post.Solution}");
            writer.WriteLine($"Upvotes: {post.UpVotes}");
            writer.WriteLine($"Downvotes {post.DownVotes}");
            writer.WriteLine("");
            writer.WriteLine("Kommentarer:");
            if (post.Comments != null)
            {
                foreach (var comment in post.Comments)
                {
                    writer.WriteLine(comment.FirstName + " " + comment.LastName);
                    writer.WriteLine(comment.Text);
                    writer.WriteLine("");
                }
            }
            else
            {
                writer.WriteLine("Denne post har ingen kommentar");
                writer.WriteLine("");
            }
            writer.WriteLine("-----------------------------------------------");
        }

        writer.Flush();
        memoryStream.Position = 0;

        return memoryStream;
    }
}