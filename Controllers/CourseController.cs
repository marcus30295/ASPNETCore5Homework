using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETcore5Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace ASPNETcore5Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public CourseController(ContosoUniversityContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCourse")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            return await _context.Courses.Where(x=>x.IsDeleted == false).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        // PUT: api/Course/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            var c = _context.Courses.Find(id);
            //valueInjecter
            c.InjectFrom(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Course
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Created("/api/Course/" + course.CourseId, course);
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var c = _context.Courses.Find(id);
            c.IsDeleted = true;

            _context.InjectFrom(c);

            await _context.SaveChangesAsync();

            return Ok(c);
        }


        [HttpGet]
        [Route("vwCourseStudent")]
        public async Task<ActionResult<IEnumerable<VwCourseStudent>>> vwCourseStudent()
        {
            return await _context.VwCourseStudents.ToListAsync();

        }

        [HttpGet]
        [Route("vwCourseStudentCount")]
        public async Task<ActionResult<IEnumerable<VwCourseStudentCount>>> vwCourseStudentCount()
        {
            return await _context.VwCourseStudentCounts.ToListAsync();

        }


        [HttpGet]
        [Route("VwDepartmentCourseCount")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> VwDepartmentCourseCount()
        {
            return await _context.VwDepartmentCourseCounts.ToListAsync();

        }

        [HttpGet]
        [Route("Query/VwDepartmentCourseCount")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> QueryVwDepartmentCourseCount()
        {

            var c = await _context.VwDepartmentCourseCounts.FromSqlRaw($"Select * from VwDepartmentCourseCount").ToArrayAsync();
            return c;

        }
    }
}