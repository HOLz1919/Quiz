using Quiz.Shared.Responses;
using Quiz.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(RegistrationModel registrationModel);
        Task<AuthResponseDto> Login(LoginModel loginModel);
        Task Logout();
    }
}
