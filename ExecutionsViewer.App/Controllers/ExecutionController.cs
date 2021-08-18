using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.Base;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database;
using ExecutionsViewer.App.Database.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionsViewer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutionController : AppControllerBase<ExecutionController>
    {
        public ExecutionController(ExecutionsViewerDbContext db, ILogger<ExecutionController> log,
            IWebHostEnvironment env) : base(db, log, env)
        {
        }


        [HttpDelete("DeleteExecution")]
        public async Task<ActionResult> DeleteExecution([FromQuery] int executionId)
        {
            var execution = await db.Executions.FindAsync(executionId);
            if (execution == null)
                return BadRequest("Invalid execution id provided");

            db.Entry(execution).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("GetExecutions")]
        public async Task<ActionResult<ICollection<Execution>>> GetExecutions
        (
            [FromQuery] int? testClassId,
            [FromQuery] int? requirementId,
            [FromQuery] int? versionId)
        {


            var query = db.Tests
                .Include(t => t.Executions)
                .AsQueryable();

            

            if (testClassId != null)
                query = query.Where(t => t.TestClassId == testClassId);

            if (requirementId != null)
                query = query.Where(t => t.Features.Any(f => f.Id == requirementId));

            if (versionId != null)
                query = query.Where(t => t.Executions.Any(e => e.Version.Id == versionId));


            var tests = await query.ToArrayAsync();


            // var query = db.Executions.AsQueryable();
            // if (includeTestClass)
            //     query = query.Include(e => e.TestClass);
            // if (includeFeatures)
            //     query = query.Include(e => e.Features);
            //
            // var executions = await query.ToArrayAsync();

            // var executions = await db.Executions.Where(e => e.ExecutionId == null)
            // .Include(e => e.TestClass)
            // .Include(e => e.ChildExecutions).ToListAsync();



            // return Ok(executions);

            return Ok(tests);
        }

        [HttpGet("GetExecution")]
        public ActionResult<ICollection<ExecutionResultDTO>> GetExecution([FromQuery] int executionId)
        {

            var execution = db.Executions
                .Include(e => e.Test)
                .AsEnumerable()
                .Where(e => e.Id == executionId)
                .ToList()
                .FirstOrDefault();

            if (execution == null)
                return NotFound();


            var executionResultDto = new ExecutionResultDTO(execution);

            return Ok(executionResultDto);
        }

    }
}