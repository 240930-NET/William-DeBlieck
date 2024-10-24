using GreaterGradesBackend.Api.Models;
using GreaterGradesBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreaterGradesBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAllAssignments()
        {
            var assignments = await _assignmentService.GetAllAssignmentsAsync();
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentDto>> GetAssignmentById(int id)
        {
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment);
        }

        [HttpPost]
        public async Task<ActionResult<AssignmentDto>> CreateAssignment(CreateAssignmentDto createAssignmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAssignment = await _assignmentService.CreateAssignmentAsync(createAssignmentDto);
            return CreatedAtAction(nameof(GetAssignmentById), new { id = createdAssignment.AssignmentId }, createdAssignment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, UpdateAssignmentDto updateAssignmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _assignmentService.UpdateAssignmentAsync(id, updateAssignmentDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var result = await _assignmentService.DeleteAssignmentAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
