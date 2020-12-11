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
    public class DepartmentsController : ControllerBase
    {
        private readonly ContosouniversityContext _context;
        private readonly ContosouniversityContextProcedures _Sp;
        public DepartmentsController(ContosouniversityContext context,ContosouniversityContextProcedures Sp)
        {
            _context = context;
            _Sp = Sp;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Department.Where(x => x.IsDeleted == false).ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            var c = _context.Department.Find(id);
            //valueInjecter
            _context.InjectFrom(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new {id = department.DepartmentId}, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            department.IsDeleted = true;

            _context.InjectFrom(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }

        [HttpPost("~/apiPostBySP/[controller]")]
        public ActionResult<Department> PostDepartmentBySP(Department model)
        {

            var result = _Sp.Department_Insert(model.Name, model.Budget, model.StartDate, model.InstructorId, null);

            return Created("/api/Department", result);
        }


        [HttpPut("~/apiPutBySP/[controller]")]
        public IActionResult InsertDepartmentBySP(int id, Department model)
        {
            var result = _Sp.Department_Update(id, model.Name, model.Budget, model.StartDate, model.InstructorId, null);

            return NoContent();
        }



        [HttpDelete("~/apiDeleteBySP/[controller]")]
        public ActionResult<Department> DeleteDepartmentBySP(int id)
        {
            var data = _context.Department.Find(id);

            var result = _Sp.Department_Delete(id, data.RowVersion);

            return Ok(data);
        }

    
    }
}