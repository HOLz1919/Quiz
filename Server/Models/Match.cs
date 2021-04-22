using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Models
{
    public class Match
    {

        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Stawka jest wymagana")]
        public int Bid { get; set; }
        [Range(1, 4, ErrorMessage = "Wartość powinna być w przedziale od 1 do 4")]
        public int Status { get; set; }
        [Required(ErrorMessage = "Maksymalna liczba graczy jest wymagana")]
        [Range(1, 10, ErrorMessage = "Maksymalna liczba graczy wynosi 10")]
        public int MaxCountOfPlayers { get; set; }
        [Required(ErrorMessage = "Kategoria jest wymagana")]
        public Guid CategoryId { get; set; }
        public Guid? OwnerId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<UserMatch> UserMatches { get; set; }
        public virtual ICollection<MatchQuestion> MatchQuestions { get; set; }

    }
}
