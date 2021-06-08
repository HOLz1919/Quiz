using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Shared.ViewModels
{
    public class AddUserVM
    {
        [MaxLength(256)]
        public string FirstName { get; set; }
        [MaxLength(256)]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Podaj Kwotę")]
        [Range(0, int.MaxValue)]
        public int Money { get; set; }
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [MinLength(4, ErrorMessage = "Nazwa musi zawierać minimum 4 znaki")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Musisz podać hasło")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Hasło musi zawierać minimum 4 znaki")]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Hasła muszą być takie same")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Adres Email jest wymagany")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Podaj prawidłowy adres Email")]
        public string Email { get; set; }

    }
}
