using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GenZHelpAPI.Data;
using GenZHelpAPI.Models;

namespace GenZHelpAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        // GET ALL STUDENTS

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET STUDENT BY ID

        [HttpGet("{id}")]

        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student =
                await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // CREATE STUDENT

        [HttpPost]

        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return Ok(student);
        }

        // UPDATE STUDENT

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateStudent(
            int id,
            Student updatedStudent
        )
        {
            if (id != updatedStudent.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedStudent).State =
                EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE STUDENT

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student =
                await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}