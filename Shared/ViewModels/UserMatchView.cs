using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class UserMatchView
    {
        public Guid MatchId { get; set; }
        public string ApplicationUserId { get; set; }
        public string Username { get; set; }
        public int Points { get; set; }

    }
}
