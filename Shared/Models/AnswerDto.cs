using System;
using System.ComponentModel.DataAnnotations;

namespace Quiz.Shared
{
    public class AnswerDto
    {

        [Key]
        public Guid  Id { get; set; }
        [MaxLength(600, ErrorMessage ="Opdowiedź może wynosić 1000 znaków")]
        [Required(ErrorMessage ="Musisz podać treść odpowiedzi")]
        public string Content { get; set; }
        public Guid QuestionId { get; set; }
        public bool IsCorrect { get; set; }


    }
}