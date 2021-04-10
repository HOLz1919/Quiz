using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared
{
    public class MatchDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Stawka jest wymagana")]
        public int Bid { get; set; }
        [Range(1,3,ErrorMessage ="Wartość powinna być w przedziale od 1 do 3")]
        public int Status { get; set; }
        [Required(ErrorMessage ="Maksymalna liczba graczy jest wymagana")]
        [Range(1,10, ErrorMessage ="Maksymalna liczba graczy wynosi 10")]
        public int MaxCountOfPlayers { get; set; }
        [Required(ErrorMessage ="Kategoria jest wymagana")]
        public Guid CategoryId { get; set; }
        


    }
}
