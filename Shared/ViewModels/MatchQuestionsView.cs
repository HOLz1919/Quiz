using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class MatchQuestionsView
    {
        public Guid MatchId { get; set; }
        public Guid QuestionId { get; set; }
        public string Content { get; set; }

        public ICollection<AnswerVM> Answers { get; set; }
    }
}
