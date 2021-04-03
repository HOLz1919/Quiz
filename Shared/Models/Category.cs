using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256)]
        [Required(ErrorMessage ="Nazwa Kategorii jest wymagana")]
        [Display(Name="Nazwa kategorii")]
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }

    }
}
