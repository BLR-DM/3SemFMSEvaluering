using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Domain.Entities.PostEntities
{
    public class Comment : DomainEntity
    {
        public string Text { get; protected set; }
        public DateTime CreatedDate { get; protected set; }

        protected Comment() { }

        private Comment(string text)
        {
            Text = text;
            CreatedDate = DateTime.Now;
        }

        public static Comment Create(string text)
        {
            return new Comment(text);
        }

        public void Update(string text)
        {
            Text = text;
        }
    }
}
