using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Controllers.Base;
using ExecutionsViewer.App.Database;
using ExecutionsViewer.App.Database.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionsViewer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : AppControllerBase<VersionController>
    {
        public VersionController(ExecutionsViewerDbContext db, ILogger<VersionController> log, IWebHostEnvironment env) : base(db, log, env)
        {
        }


        [HttpGet("GetVersions")]
        public async Task<ActionResult<ICollection<Version>>> GetTestClasses()
        {
            var versions = await db.Versions
                .OrderByDescending(v => v.Id)
                .ToListAsync();
            return Ok(versions);
        }

    }
}