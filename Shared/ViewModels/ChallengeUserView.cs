using Quiz.Shared.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class ChallengeUserView
    {
        public Guid ChallengeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Count { get; set; }
        public int Reward { get; set; }
        public string UserId { get; set; }
        public int? WonMatches { get; set; }
        public int Status { get; set; }
        public double Percentage { get; set; }


    }
}
