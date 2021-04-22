using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Models
{
    public class MatchQuestion
    {

        public Guid MatchId { get; set; }
        public virtual Match Match { get; set; }

        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }

    }
}
