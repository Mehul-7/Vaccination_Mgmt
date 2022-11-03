using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vaccination_Mgmt.Data;
using Vaccination_Mgmt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Vaccination_Mgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosageController : ControllerBase
    {
        VacDbContext _dbContext = new VacDbContext();
        [HttpPost("[action]")]
        [Authorize]
        public IActionResult Dose_Details([FromBody] Dose Dose)
        {
            int CurrentUserId = Int32.Parse(HttpContext.Request.Cookies["id"]!);
            var DoseExists = _dbContext.DoseMappings.FirstOrDefault(s => s.UserId == CurrentUserId);
            if (DoseExists == null)
            {
                Dose NewDose = new Dose()
                {
                    Id = Dose.Id,
                    Date = Dose.Date,
                    Type = Dose.Type,
                };
                _dbContext.Doses.Add(NewDose);
                _dbContext.SaveChanges();
                DoseMapping NewMapping = new DoseMapping()
                {
                    UserId = CurrentUserId,
                    DoseId = Dose.Id,
                    VaccineId = Dose.VaccineId
                };
                _dbContext.DoseMappings.Add(NewMapping);
                _dbContext.SaveChanges();
            }
            else
            {
                var CurrrentDose = _dbContext.Doses.FirstOrDefault(x => x.Id == DoseExists.DoseId);
                CurrrentDose!.Date = Dose.Date;
                CurrrentDose.Type = Dose.Type;
                _dbContext.SaveChanges();
            }
            
            return StatusCode(StatusCodes.Status202Accepted);
        }

        //Edit Dose information
        [HttpPut("[action]")]
        public IActionResult Edit_Details([FromBody] Dose dose)
        {
            var AlreadyExists = _dbContext.Doses.FirstOrDefault(x => x.Id == dose.Id);
            if(AlreadyExists == null)
            {
                return NotFound("No such record exists");
            }
            else
            {
                if(dose.Type != 0 && dose.Type >= 1 && dose.Type <= 3)
                {
                    AlreadyExists.Type = dose.Type;
                }
                if(dose.Date != new DateTime(0001, 01, 01))
                {
                    AlreadyExists.Date = dose.Date;
                }
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        //Delete a dose record
        [HttpDelete("[action]")]
        [Authorize]
        public IActionResult Delete_Dose(int id)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"execute DeleteDose @DoseId={id};");
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
