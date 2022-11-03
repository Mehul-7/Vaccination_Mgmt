using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaccination_Mgmt.Data;
using Vaccination_Mgmt.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vaccination_Mgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        VacDbContext _dbContext = new VacDbContext();

        // View all available roles
        [HttpGet]
        [Authorize]
        
        public IActionResult Get()
        {
            var Roles = _dbContext.Roles.
                        Select(x => new
                        {
                            Id = x.Id,
                            Title = x.Title,
                        });
            return Ok(Roles);
        }

        // Search for a role
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var Roles = _dbContext.Roles.Where(x => x.Id == id).
                        Select(x => new
                        {
                            Id = x.Id,
                            Title = x.Title,
                        });
            if (Roles.Count() == 0) return NotFound();
            else return Ok(Roles);
        }

        // Enter new Role 
        [HttpPost("[action]")]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public IActionResult Add_Role([FromBody] Role role)
        {
            var AlreadyExists = _dbContext.Roles.FirstOrDefault(x => x.Id == role.Id);
            if(AlreadyExists == null)
            {
                _dbContext.Roles.Add(role);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        //Edit Role Details
        [HttpPut("[action]")]
        [Authorize]
        public IActionResult EditDetails([FromBody] Role role)
        {
            var AlreadyExists = _dbContext.Roles.FirstOrDefault(x => x.Id == role.Id);
            if(AlreadyExists != null)
            {
                AlreadyExists.Title = role.Title;
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                return NotFound();
            }

        }

        // Remove a Role 
        [HttpDelete("[action]")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"execute DeleteRole @RoleId={id};");
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
