using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniJira.API.Data;
using MiniJira.API.Models;

namespace MiniJira.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _context.Projects
                                         .Include(p => p.Issues)
                                         .ToListAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _context.Projects
                                        .Include(p => p.Issues)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Project project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
                return BadRequest("Project name is required.");

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Project updatedProject)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
