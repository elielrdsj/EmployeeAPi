using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NovoProjeto.Filters;
using NovoProjeto.Interfaces;
using NovoProjeto.Models;
using System.Text.Json;
using NovoProjeto.Models;
using System.Linq;
using EmployeeManagementAPI.Context;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace NovoProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NovoProjetoController : ControllerBase
    {

        private readonly ILogger<NovoProjetoController> _logger;
        private readonly List<Employee> _database;
        private readonly IRepository _repository;

        public NovoProjetoController(ILogger<NovoProjetoController> logger, IRepository repository)
        {
            _logger = logger;
            _database = new List<Employee>();
            _repository = repository;
        }
        private readonly DataContext context;
        public NovoProjetoController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(204)]
        [CustomActionFilter]
        public IActionResult Get(int page, int maxResults)
        {
            return Ok(_repository.Get(page, maxResults));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int Id)
        {
            var employee = await context.EmployeeDb.FindAsync(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        public async Task<ActionResult<List<Employee>>> Post(Employee employee)
        {
            context.EmployeeDb.Add(employee);
            await context.SaveChangesAsync();
            return Ok(await context.EmployeeDb.ToListAsync());
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        public async Task<ActionResult<List<Employee>>> Put(Employee request)
        {
            var employee = await context.EmployeeDb.FindAsync(request.Id);
            if (employee == null)
            {
                return BadRequest();
            }
            employee.Name = request.Name;
            employee.StartDate = request.StartDate;
            employee.EndDate = request.EndDate;
            employee.WorkSector = request.WorkSector;
            employee.SocialSecurity = request.SocialSecurity;
            employee.Salary = request.Salary;
            employee.Function = request.Function;
            await context.SaveChangesAsync();
            return Ok(await context.EmployeeDb.ToListAsync());
        }

        [HttpPatch("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<List<Employee>>> Patch(EmployeePatch patch, int Id)
        {
            var employee = await context.EmployeeDb.FindAsync(Id);
            if (employee == null)
            {
                return BadRequest();
            }
            if (patch.Name != "")
            {
                employee.Name = patch.Name;
            }
            if (patch.Salary != 0)
            {
                employee.Salary = patch.Salary;
            }
            if (patch.WorkSector != "")
            {
                employee.WorkSector = patch.WorkSector;
            }
            if (patch.SocialSecurity != 0)
            {
                employee.SocialSecurity = patch.SocialSecurity;
            }
            return Ok(await context.EmployeeDb.ToListAsync());
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<List<Employee>>> Delete(Employee request)
        {
            var employee = await context.EmployeeDb.FindAsync(request.Id);
            if (employee == null)
            {
                return BadRequest();
            }
            context.EmployeeDb.Remove(employee);
            return Ok();
        }

        [HttpPost("{name}")]
        [Authorize]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(204)]

        public async Task<ActionResult<List<Employee>>> SearchByName([FromQuery] int page, [FromBody] string Name, [FromQuery] int maxObj = 10)
        {
            var query = context.EmployeeDb.Select(x => x.Name.Contains(Name));
            if (query == null)
            {
                return NotFound();
            }
            if (page == 1)
            {
                query = query.Take(maxObj * page);
            }
            else
            {
                query = query.Skip(maxObj * page - 1).Take(maxObj);
            }
            return Ok(query);
        }
        [HttpPost("{worksector}")]
        [Authorize]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<List<Employee>>> SearchWorkSector([FromQuery] int page, [FromBody] string workSector, [FromQuery] int maxObj = 10)
        {
            var query = context.EmployeeDb.Select(x => x.WorkSector == workSector);
            if (query == null)
            {
                return NotFound();
            }
            if (page == 1)
            {
                query = query.Take(maxObj * page);
            }
            else
            {
                query = query.Skip(maxObj * page - 1).Take(maxObj);
            }
            return Ok(query);
        }
    }
}
