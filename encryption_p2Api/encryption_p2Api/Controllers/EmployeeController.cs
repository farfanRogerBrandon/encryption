using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace encryption_p2Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context) => _context = context;

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost("GetAssistancesByEmployeeName")]
        public async Task<IActionResult> GetAssistancesByEmployeeName([FromBody] string name)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.FullName.Contains(name));

            if (employee == null)
            {
                return NotFound("Empleado no encontrado");
            }

            var assistances = await _context.Asistances
                .Where(a => a.EmployeeID == employee.ID)
                .ToListAsync();

            return Ok(assistances);
        }
    }
}
