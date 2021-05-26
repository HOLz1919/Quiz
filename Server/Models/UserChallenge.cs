using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Models
{
    public class UserChallenge
    {
        public Guid ChallengeId { get; set; }
        public virtual Challenge Challenge { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int Status { get; set; }

    }
}
