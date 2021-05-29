using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Quiz.Server.Data;
using Quiz.Server.Hubs;
using Quiz.Server.Models;
using Quiz.Server.Services;
using Quiz.Shared.Models;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChallengeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IChallengeService _challengeService;


        public ChallengeController(ApplicationDbContext db, IChallengeService challengeService)
        {
            _db = db;
            _challengeService = challengeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _challengeService.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var category = await _challengeService.GetAsync(id);
            if (category == null)
                return NotFound(new { ErrorMessage = "Not found category with that id" });
            return Ok(category);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Challenge challenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }

            var result = await _challengeService.AddAsync(challenge);

            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _challengeService.DeleteAsync(id);
            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] Challenge challenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }

            var result = await _challengeService.EditAsync(challenge);

            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetChallengeUser/{id}")]
        public async Task<IActionResult> GetChallengeUser(string id)
        {
            var challenges = await _challengeService.GetChallengeUserAsync(id);
            if (challenges == null)
                return NotFound(new { ErrorMessage = "Not found challenges" });


            return Ok(challenges);

        }

        [HttpPatch("EndChallenge")]
        public async Task<IActionResult> EndChallenge([FromBody] UserChallenge userChallenge)
        {

            var result = await _challengeService.EndChallenge(userChallenge);

            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }


    }
}
