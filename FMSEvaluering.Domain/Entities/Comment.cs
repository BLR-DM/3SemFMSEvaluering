using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSEvaluering.Domain.Entities
{
    public class Comment : DomainEntity
    {
        public string Text { get; protected set; }

        protected Comment() {}

        private Comment(string text)
        {
            Text = text;
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
