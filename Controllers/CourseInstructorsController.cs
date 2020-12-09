using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNETcore5Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace ASPNETcore5Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInstructorsController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public CourseInstructorsController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET: api/CourseInstructors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseInstructor>>> GetCourseInstructors()
        {
            return await _context.CourseInstructors.ToListAsync();
        }

        // GET: api/CourseInstructors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseInstructor>> GetCourseInstructor(int id)
        {
            return await _context.CourseInstructors.FindAsync(id);
        }

        // PUT: api/CourseInstructors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseInstructor(int id, CourseInstructor courseInstructor)
        {
            var c = _context.CourseInstructors.Find(id);
            //valueInjecter
            _context.InjectFrom(courseInstructor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/CourseInstructors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseInstructor>> PostCourseInstructor(CourseInstructor courseInstructor)
        {
            _context.CourseInstructors.Add(courseInstructor);

            await _context.SaveChangesAsync();


            return CreatedAtAction("GetCourseInstructor", new {id = courseInstructor.CourseId}, courseInstructor);
        }

        // DELETE: api/CourseInstructors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseInstructor(int id)
        {
            var courseInstructor = await _context.CourseInstructors.FindAsync(id);


            _context.CourseInstructors.Remove(courseInstructor);
            await _context.SaveChangesAsync();

            return Ok(courseInstructor);
        }
    }
}