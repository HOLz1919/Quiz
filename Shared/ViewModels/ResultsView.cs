using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class ResultsView
    {
        public int Place { get; set; }
        public string Username { get; set; }
        public string ApplicationUserId { get; set; }
        public int WonMatches { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
