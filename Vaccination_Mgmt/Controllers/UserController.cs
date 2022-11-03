using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vaccination_Mgmt.Data;
using Vaccination_Mgmt.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Vaccination_Mgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        VacDbContext _dbContext = new VacDbContext();
        private IConfiguration _config;

        public UsersController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get_List(int id)
        {
            var UserList = _dbContext.Users.
                           Select (x => new
                           {
                               Contact = x.Contact,
                               Name = x.UserName
                           });
            return Ok(UserList);
        }

        //Getting the User Details
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get_Details(int id)
        {
            var details = from user in _dbContext.Users
                          join d in _dbContext.DoseMappings on user.Id equals d.UserId
                          join dose in _dbContext.Doses on d.DoseId equals dose.Id
                          join v in _dbContext.Vaccines on d.VaccineId equals v.Id
                          where user.Id == id
                          select new
                          {
                              Id = user.Id,
                              Name = user.UserName,
                              DoB = user.Dob,
                              Contact = user.Contact,
                              Gender = user.Gender,
                              Date_Of_Vaccination = dose.Date,
                              Type = dose.Type,
                              Vaccine = v.Name
                          };


            return Ok(details);

        }

        //Adding a new User
        [HttpPost("[action]")]
        [Authorize]
        public IActionResult Register([FromBody] User obj)
        {
            var UserExists = _dbContext.Users.FirstOrDefault(u => u.Contact == obj.Contact);
            if (UserExists != null)
            {
                return BadRequest("Mobile no. already registered");
            }
            else
            {
                _dbContext.Users.Add(obj);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created);
            }
        }

        //Login as an existing User
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] User obj)
        {
            var CurrentUser = _dbContext.Users.FirstOrDefault(u => u.Contact == obj.Contact && u.Password == obj.Password);
            if (CurrentUser == null)
            {
                return NotFound();
            }
            else
            {
                var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
                var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.MobilePhone, obj.Contact),
                };
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials);
                    
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                CookieOptions options = new CookieOptions();
                options.HttpOnly = true;
                options.Secure = true;
                options.SameSite = SameSiteMode.Strict;
                HttpContext.Response.Cookies.Append("id", jwt, options);
                return Ok(StatusCode(StatusCodes.Status202Accepted));
            }
        }

        //Editing the details for an existing user
        [HttpPut("[action]")]
        [Authorize]
        public IActionResult EditDetails(string contact, [FromBody] User user)
        {
            var CurrentDetails = _dbContext.Users.FirstOrDefault(u => u.Contact == contact);
            if (CurrentDetails == null)
            {
                return NotFound("User not found");
            }
            else
            {
                if (user.Contact != null) CurrentDetails.Contact = user.Contact;
                if(user.UserName!=null) CurrentDetails.UserName = user.UserName;
                if(user.Password!=null) CurrentDetails.Password = user.Password;
                if(user.Dob!=new DateTime(0001, 01, 01)) CurrentDetails.Dob = user.Dob;
                if(user.Gender!=null) CurrentDetails.Gender = user.Gender;
                _dbContext.SaveChanges();
                return Ok("Details updated");
            }
        }

        //Deleting an existing user
        [HttpDelete("[action]")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var Dose_Id=_dbContext.DoseMappings.FirstOrDefault(x => x.UserId == id);
            _dbContext.Database.ExecuteSqlInterpolated($"execute DeleteUser @UserId={id};");
            _dbContext.SaveChanges();
            _dbContext.Doses.Remove(_dbContext.Doses.FirstOrDefault(x => x.Id == Dose_Id.DoseId));
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
