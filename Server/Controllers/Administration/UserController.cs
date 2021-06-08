using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quiz.Server.Data;
using Quiz.Server.Models;
using Quiz.Server.Services;
using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
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

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddUserVM user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }

            ApplicationUser applicationUser = new ApplicationUser() { UserName = user.Username, FirstName = user.FirstName, LastName = user.LastName, Money = user.Money, Email = user.Email };
            var result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new ResponseDto { ErrorMessage = errors.ToString() });
            }

            await _userManager.AddToRoleAsync(applicationUser, "User");

            return StatusCode(201);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(new ResponseDto() { ErrorMessage=result.Errors.ToString()});

            return Ok(result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] UserVM user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }
            var userDB = await _userManager.FindByIdAsync(user.Id);
            if (userDB == null)
            {
                return BadRequest("User Not Found");
            }

            userDB.UserName = user.Username;
            userDB.Money = user.Money;
            userDB.Email = user.Email;
            userDB.FirstName = user.FirstName;
            userDB.LastName = user.LastName;
            var result = await _userManager.UpdateAsync(userDB);


            if (!result.Succeeded)
                return BadRequest(new ResponseDto() { ErrorMessage = result.Errors.ToString() });

            return Ok(result);
        }







    }
}
