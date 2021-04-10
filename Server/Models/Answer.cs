using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Models
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(600, ErrorMessage = "Opdowiedź może wynosić 1000 znaków")]
        [Required(ErrorMessage = "Musisz podać treść odpowiedzi")]
        public string Content { get; set; }
        public Guid QuestionId { get; set; }
        public bool IsCorrect { get; set; }


        public virtual Question Question { get; set; }
    }
}
