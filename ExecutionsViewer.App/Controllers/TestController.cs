using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.Base;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database;
using ExecutionsViewer.App.Database.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionsViewer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : AppControllerBase<TestController>
    {
        public TestController(ExecutionsViewerDbContext db, ILogger<TestController> log, IWebHostEnvironment env) : base(
            db, log, env)
        {
        }


        [HttpGet("GetTestsSummary")]
        public async Task<ActionResult<ICollection<TestSummaryDTO>>> GetTestsSummary
        (
            [FromQuery] int? versionId,
            [FromQuery] int? testClassId,
            [FromQuery] int? featureId)
        {
            var testsQ = db.Tests.AsQueryable();

            if (versionId != null)
                testsQ = db.Tests.Where(t => versionId >= t.FirstVersionId);

            if (testClassId != null)
                testsQ = testsQ.Where(t => t.TestClassId == testClassId);

            if (featureId != null)
                testsQ = testsQ
                    .Include(t => t.Features)
                    .Where(t => t.Features.Any(f => f.Id == featureId));

            testsQ = testsQ
                .Include(t => t.Executions).ThenInclude(e => e.Version)
                .Include(t => t.Features)
                .Include(t => t.TestClass);

            var tests = await testsQ.ToListAsync();

            var testsDtos = new List<TestSummaryDTO>();

            if (tests == null)
                return testsDtos;

            if (versionId == null)
            {
                testsDtos.AddRange(tests.Select(test => new TestSummaryDTO(test, test.Executions?.ToList())));
            }
            else
            {
                var testsWithExecutions = new List<TestSummaryDTO>();
                var testsWithoutExecutions = new List<TestSummaryDTO>();

                foreach (var test in tests)
                {
                    List<Execution> matchedExecutions = null;
                    if (test.Executions?.Count > 0)
                        matchedExecutions = test.Executions.Where(e => e.VersionId == versionId).ToList();


                    if (matchedExecutions?.Count > 0)
                    {
                        matchedExecutions = matchedExecutions.OrderByDescending(e => e.Id).ToList();
                        testsWithExecutions.Add(new TestSummaryDTO(test, matchedExecutions));
                    }
                    else
                        testsWithoutExecutions.Add(new TestSummaryDTO(test, null));
                }

                testsDtos.AddRange(testsWithExecutions);
                testsDtos.AddRange(testsWithoutExecutions);
            }


            return Ok(testsDtos);
        }
    }
}