using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNETcore5Homework.Models;
using Omu.ValueInjecter;

namespace ASPNETcore5Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeAssignmentsController : ControllerBase
    {
        private readonly ContosouniversityContext _context;

        public OfficeAssignmentsController(ContosouniversityContext context)
        {
            _context = context;
        }

        // GET: api/OfficeAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfficeAssignment>>> GetOfficeAssignments()
        {
            return await _context.OfficeAssignment.ToListAsync();
        }

        // GET: api/OfficeAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeAssignment>> GetOfficeAssignment(int id)
        {
            var officeAssignment = await _context.OfficeAssignment.FindAsync(id);


            return officeAssignment;
        }

        // PUT: api/OfficeAssignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfficeAssignment(int id, OfficeAssignment officeAssignment)
        {
            var c = _context.OfficeAssignment.Find(id);
            //valueInjecter
            _context.InjectFrom(officeAssignment);


            await _context.SaveChangesAsync();
            
         
            return NoContent();
        }

        // POST: api/OfficeAssignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OfficeAssignment>> PostOfficeAssignment(OfficeAssignment officeAssignment)
        {
            _context.OfficeAssignment.Add(officeAssignment);
            
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetOfficeAssignment", new { id = officeAssignment.InstructorId }, officeAssignment);
        }

        // DELETE: api/OfficeAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficeAssignment(int id)
        {
            var officeAssignment = await _context.OfficeAssignment.FindAsync(id);

            _context.OfficeAssignment.Remove(officeAssignment);
            await _context.SaveChangesAsync();

            return Ok(officeAssignment);
        }

     
    }
}
