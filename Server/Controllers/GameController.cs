using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Server.Data;
using Quiz.Server.Models;
using Quiz.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IGameService _matchService;

        public GameController(ApplicationDbContext db, IGameService matchService)
        {
            _db = db;
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _matchService.GetAsync();
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var question = await _questionService.GetAsync(id);
        //    if (question == null)
        //        return NotFound(new { ErrorMessage = "Not found question with that id" });
        //    return Ok(question);
        //}

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }

            var result = await _matchService.AddAsync(match);

            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }


        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{

        //    var result = await _questionService.DeleteAsync(id);
        //    if (!result.IsSuccessful)
        //        return BadRequest(result);

        //    return Ok(result);
        //}

        //[HttpPut("Edit")]
        //public async Task<IActionResult> Edit([FromBody] QuestionVM questionVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new { ErrorMessage = "Model is not Valid" });
        //    }

        //    var result = await _questionService.EditAsync(questionVM);

        //    if (!result.IsSuccessful)
        //        return BadRequest(result);

        //    return Ok(result);
        //}






    }
}
