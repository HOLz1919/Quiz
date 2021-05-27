using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class ChallengeView
    {
   
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
        public int Reward { get; set; }
    }
}
