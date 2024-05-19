using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsightGlassTest.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightGlassTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly idbcontext _context;

        public JobsController(idbcontext context)
        {
            _context = context;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<JobDto>>> SearchJobs(string search)
        {
            var jobs = await _context.Jobopenings
                                     .Include(j => j.JobCompanyUser)
                                     .Where(j => j.JobTitle.Contains(search) || j.JobDetails.Contains(search))
                                     .Select(j => new JobDto
                                     {
                                         JobId = j.JobId,
                                         JobTitle = j.JobTitle,
                                         JobDetails = j.JobDetails,
                                         JobSalary = j.JobSalary.ToString(),
                                         EmploymentType = j.JobEmploymentType
                                     })
                                     .ToListAsync();

            return Ok(jobs);
        }
    }
}
