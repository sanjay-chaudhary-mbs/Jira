using Microsoft.AspNetCore.Mvc;
using MiniJira.API.Models;
using MiniJira.API.Services.Interface;

namespace MiniJira.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _service;

        public IssuesController(IIssueService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var issues = await _service.GetAllAsync();
            return Ok(issues);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _service.GetByIdAsync(id);
            if (issue == null)
                return NotFound($"No issue found with ID {id}");
            return Ok(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Issue issue)
        {
            var created = await _service.CreateAsync(issue);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Issue issue)
        {
            if (id != issue.Id) return BadRequest("ID mismatch.");
            var updated = await _service.UpdateAsync(issue);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound($"Issue with ID {id} not found.");
        }
    }
}
