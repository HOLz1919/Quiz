using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Models
{
    public class UserMatch
    {

        public Guid MatchId { get; set; }
        public virtual Match Match { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int Points { get; set; }

    }
}
