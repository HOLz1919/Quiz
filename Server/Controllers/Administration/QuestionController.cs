using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Server.Data;
using Quiz.Server.Services;
using Quiz.Shared;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;

        public QuestionController(ApplicationDbContext db, ICategoryService categoryService, IQuestionService questionService)
        {
            _db = db;
            _categoryService = categoryService;
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _questionService.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var question = await _questionService.GetAsync(id);
            if (question == null)
                return NotFound(new { ErrorMessage = "Not found question with that id" });
            return Ok(question);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] QuestionVM questionVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }

            var result = await _questionService.AddAsync(questionVM);

            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _questionService.DeleteAsync(id);
            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] QuestionVM questionVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }

            var result = await _questionService.EditAsync(questionVM);

            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }





    }
}
