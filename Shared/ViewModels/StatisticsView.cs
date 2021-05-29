using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class StatisticsView
    {
        public string ApplicationUserId { get; set; }
        public int WonMatches { get; set; }
        public int MatchCount { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
