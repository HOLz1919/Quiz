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
        [Range(0,int.MaxValue, ErrorMessage ="Stawka musi być nieujemna")]
        public int Bid { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage ="Maksymalna liczba graczy jest wymagana")]
        [Range(1,10, ErrorMessage ="Maksymalna liczba graczy wynosi 10")]
        public int MaxCountOfPlayers { get; set; }
        [Required(ErrorMessage ="Kategoria jest wymagana")]
        public Guid CategoryId { get; set; }
        public Guid? OwnerId { get; set; }



    }
}
