using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSExitSlip.Domain.Entities
{
    public class Question : DomainEntity
    {
        private readonly List<Response> _responses = [];
        public string Text { get; protected set; }
        public string AppUserId { get; protected set; }
        public IReadOnlyCollection<Response> Responses => _responses;

        protected Question() { }
        private Question(string text, string appUserId)
        {
            Text = text;
            AppUserId = appUserId;
        }

        public static Question Create(string text, string appUserId)
        {
            return new Question(text, appUserId);
        }

        public void AddResponse(string text, string appUserId)
        {
            var response = Response.Create(text, appUserId, Responses);
            _responses.Add(response);
        }

        public Response UpdateResponse(int responseId, string text)
        {
            var response = Responses.FirstOrDefault(r => r.Id == responseId);
            if (response == null) throw new ArgumentException("Response not found");
            response.Update(text);
            return response;
        }
    }
}
