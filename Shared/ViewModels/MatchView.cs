using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class MatchView
    {
        public Guid Id { get; set; }
        public int Bid { get; set; }
        public int Status { get; set; }
        public int CountOfPlayers { get; set; }
        public int MaxCountOfPlayers { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set;  }
        public Guid OwnerId { get; set; }
        public List<UserMatchView> Players { get; set; }
        
        

    }
}
