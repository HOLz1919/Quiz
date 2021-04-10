using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256)]
        [Required(ErrorMessage = "Nazwa Kategorii jest wymagana")]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}
