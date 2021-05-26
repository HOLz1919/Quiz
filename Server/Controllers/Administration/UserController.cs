using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quiz.Server.Data;
using Quiz.Server.Models;
using Quiz.Server.Services;
using Quiz.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Controllers.Administration
{
 
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this._db = db;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._userService = userService;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAsync();
            return Ok(result);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _userService.GetAsync(id);
            if (result == null)
                return NotFound(new { ErrorMessage = "Not found user with that id" });
            return Ok(result);
        }


        [HttpGet("GetUserMoney")]
        public async Task<IActionResult> GetUserMoney(string UserId)
        {
            var result = await _userService.GetMoney(UserId);
            if (!result.IsSuccessful)
                return NotFound(result);
            return Ok(result);
        }

        




    }
}
