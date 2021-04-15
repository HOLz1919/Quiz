using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.Models
{
    public class UserMatchDto
    {
        public Guid matchId { get; set; }
        public string userId { get; set; }
    }
}
