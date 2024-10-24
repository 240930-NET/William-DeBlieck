using GreaterGradesBackend.Api.Models;
using GreaterGradesBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreaterGradesBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetAllClasses()
        {
            var classes = await _classService.GetAllClassesAsync();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassDto>> GetClassById(int id)
        {
            var classDto = await _classService.GetClassByIdAsync(id);
            if (classDto == null)
            {
                return NotFound();
            }
            return Ok(classDto);
        }

        [HttpPost]
        public async Task<ActionResult<ClassDto>> CreateClass(CreateClassDto createClassDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdClass = await _classService.CreateClassAsync(createClassDto);
            return CreatedAtAction(nameof(GetClassById), new { id = createdClass.ClassId }, createdClass);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(int id, UpdateClassDto updateClassDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _classService.UpdateClassAsync(id, updateClassDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var result = await _classService.DeleteClassAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost("{id}/students/{studentId}")]
        public async Task<IActionResult> AddStudentToClass(int id, int studentId)
        {
            var result = await _classService.AddStudentToClassAsync(id, studentId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}/students/{studentId}")]
        public async Task<IActionResult> RemoveStudentFromClass(int id, int studentId)
        {
            var result = await _classService.RemoveStudentFromClassAsync(id, studentId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
