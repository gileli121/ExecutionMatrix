using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExecutionMatrix.App.Controllers.Base;
using ExecutionMatrix.App.Controllers.DTOs;
using ExecutionMatrix.App.Database;
using ExecutionMatrix.App.Database.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionMatrix.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : AppControllerBase<VersionController>
    {
        public VersionController(ExecutionMatrixDbContext db, ILogger<VersionController> log, IWebHostEnvironment env) : base(db, log, env)
        {
        }


        [HttpGet("GetVersions")]
        public async Task<ActionResult<ICollection<Version>>> GetTestClasses()
        {
            var versions = await db.Versions.ToListAsync();
            versions.OrderByDescending(v => v.Id);
            return Ok(versions);
        }

    }
}