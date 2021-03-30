using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Server.Data;
using Quiz.Server.Services;
using Quiz.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly ICategoryService categoryService;

        public CategoryController(ApplicationDbContext db, ICategoryService categoryService)
        {
            this.db = db;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(categoryService.Get());
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var category = categoryService.Get(id);
            if (category == null)
                return NotFound(new { ErrorMessage = "Not found category with that id" });
            return Ok(category);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }

            var result = await categoryService.AddAsync(category);

            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await categoryService.DeleteAsync(id);
            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }

            var result = await categoryService.EditAsync(category);

            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }



    }
}
