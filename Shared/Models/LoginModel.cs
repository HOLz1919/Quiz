using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Musisz podać hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
