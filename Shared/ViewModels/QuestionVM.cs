using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class QuestionVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Treść jest wymagana")]
        [Display(Name = "Treść pytania")]
        [MaxLength(1500)]
        public string Content { get; set; }
        [Required(ErrorMessage = "Musisz Wybrać kategorię")]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
