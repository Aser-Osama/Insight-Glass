using InsightGlassTest.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsightGlassTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly idbcontext _context;

        public RegisterController(idbcontext context)
        {
            _context = context;
        }

        // GET api/<RegisterController>/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return "PLEASE PROVIDE EMAIL";
            
            var user = _context.Aspnetusers.FirstOrDefault(x => x.Id == id);
            if (user is null)
                return "user not found";

            //if (!user.LoggedInBefore)
            //    return "First time login";
            
            if(!string.IsNullOrEmpty(user.UserType))
                return user.UserType;
                
            if (_context.Companies.Any(x=>x.CompanyUserId == user.Id))
            {
                user.UserType = "Company";
                _context.SaveChangesAsync();
                return "Company";
            }
            else if (_context.Seekers.Any(x=> x.SeekerUserId == user.Id))
            {
                user.UserType = "Seeker";
                _context.SaveChangesAsync();
                return "Seeker";
            }

            return "First time login";

        }
    }
}
