using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionMatrix.App.Controllers.Base;
using ExecutionMatrix.App.Controllers.DTOs;
using ExecutionMatrix.App.Database;
using ExecutionMatrix.App.Database.Tables;
using ExecutionMatrix.App.Database.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionMatrix.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : AppControllerBase<FeatureController>
    {
        public FeatureController(ExecutionMatrixDbContext db, ILogger<FeatureController> log,
            IWebHostEnvironment env) : base(
            db, log, env)
        {
        }


        [HttpGet("GetFeatures")]
        public async Task<ActionResult<ICollection<FeatureDTO>>> GetFeatures()
        {
            var features = await db.Features.ToListAsync();
            var featuresDtos = features.Select(feature => new FeatureDTO(feature)).ToList();

            return Ok(featuresDtos);
        }

        [Authorize]
        [HttpDelete("DeleteFeature")]
        public async Task<ActionResult> DeleteFeature([FromQuery] int featureId)
        {
            var feature = await db.Features.FindAsync(featureId);
            if (feature == null)
                return BadRequest("Invalid feature id provided");

            db.Entry(feature).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return Ok("Feature deleted");
        }


        [HttpGet("GetFeaturesSummary")]
        public async Task<ActionResult<ICollection<FeatureSummary>>> GetFeaturesSummary(
            [FromQuery] int versionId)
        {
            var features = await db.Features
                .Include(f => f.Tests)
                .ToListAsync();

            var executionsInVersion = await db.Executions
                .Where(e => e.ExecutionId == null)
                .Where(e => e.VersionId == versionId)
                .OrderByDescending(e => e.ExecutionDate)
                .ToListAsync();

            var featureSummaries = new List<FeatureSummary>();
            foreach (var feature in features)
            {
                var totalTests = 0;
                var totalExecutedTests = 0;
                var totalPassedTests = 0;
                foreach (var featureTest in feature.Tests)
                {
                    totalTests++;

                    var lastExInFeature = executionsInVersion.FirstOrDefault(e => e.TestId == featureTest.Id);

                    if (lastExInFeature == null)
                        continue;

                    if (lastExInFeature.ExecutionResult == ExecutionResult.Passed)
                    {
                        totalPassedTests++;
                        totalExecutedTests++;
                    }
                    else if (lastExInFeature.ExecutionResult != ExecutionResult.Skipped && lastExInFeature.ExecutionResult != ExecutionResult.UnExecuted)
                    {
                        totalExecutedTests++;
                    }

                }

                var featureSummary = new FeatureSummary(feature, totalTests, totalExecutedTests, totalPassedTests);

                featureSummaries.Add(featureSummary);
            }

            return Ok(featureSummaries);
        }
    }
}