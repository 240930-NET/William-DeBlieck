using GreaterGradesBackend.Api.Models;
using GreaterGradesBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreaterGradesBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetAllGrades()
        {
            var grades = await _gradeService.GetAllGradesAsync();
            return Ok(grades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GradeDto>> GetGradeById(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return Ok(grade);
        }

        /*[HttpPost]
        public async Task<ActionResult<GradeDto>> CreateGrade(CreateGradeDto createGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdGrade = await _gradeService.CreateGradeAsync(createGradeDto);
            return CreatedAtAction(nameof(GetGradeById), new { id = createdGrade.GradeId }, createdGrade);
        }*/

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id, UpdateGradeDto updateGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _gradeService.UpdateGradeAsync(id, updateGradeDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var result = await _gradeService.DeleteGradeAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        */
    }
}
