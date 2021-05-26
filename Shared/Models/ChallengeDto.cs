using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared
{
    public class ChallengeDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [Display(Name = "Tytuł wyzwania")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Treść jest wymagana")]
        [Display(Name = "Treść wyzwania")]
        [MaxLength(1500)]
        public string Content { get; set; }
        [Required(ErrorMessage = "Musisz Wybrać kategorię")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Ilośc jest wymagana")]
        [Display(Name = "Ilość do wykonania")]
        [Range(1, 100)]
        public int Count { get; set; }

        [Required(ErrorMessage = "Musisz wyznaczyć nagrodę")]
        [Display(Name = "Nagroda")]
        public int Reward { get; set; }

    }
}
