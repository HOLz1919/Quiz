using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class QuestionView
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
