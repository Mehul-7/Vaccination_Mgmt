using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using Vaccination_Mgmt.Data;
using Vaccination_Mgmt.Models;


namespace Vaccination_Mgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        VacDbContext _dbContext = new VacDbContext();

        // GET: api/<VaccineController>
        [HttpGet]
        public IActionResult Get()
        {
            var VaccineList = _dbContext.Vaccines.
                                Select(x => new
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                });
            return Ok(VaccineList);
        }

        // GET api/<VaccineController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var VaccineList = _dbContext.Vaccines.Where(x => x.Id == id).
                                Select(x => new
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                });
            if (VaccineList.Count() == 0)
            {
                return NotFound("No such record exists");
            }
            else
                return Ok(VaccineList);
        }

        // POST api/<VaccineController>
        [HttpPost("[action]")]
        [Authorize]
        public IActionResult Add([FromBody] Vaccine vaccine)
        {
            var AlreadyExists = _dbContext.Vaccines.FirstOrDefault(x => x.Id == vaccine.Id);
            if(AlreadyExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
            _dbContext.Vaccines.Add(vaccine);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<VaccineController>/5
        [HttpPut("[action]")]
        [Authorize]
        public IActionResult Modify(int id, [FromBody] Vaccine vaccine)
        {
            var AlreadyExists = _dbContext.Vaccines.FirstOrDefault(x => x.Id == vaccine.Id);
            if (AlreadyExists == null)
            {
                return NotFound("No such record found");
            }
            AlreadyExists.Name = vaccine.Name;
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status202Accepted);
        }
        // DELETE api/<VaccineController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
