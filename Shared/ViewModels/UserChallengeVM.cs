using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class UserChallengeVM
    {
        public Guid ChallengeId { get; set; }
        public string ApplicationUserId { get; set; }
        public int Status { get; set; }
    }
}
