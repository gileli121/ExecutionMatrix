using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;
using ExecutionsViewer.App.Controllers.Base;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database;
using ExecutionsViewer.App.Database.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionsViewer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : AppControllerBase<FeatureController>
    {
        public FeatureController(ExecutionsViewerDbContext db, ILogger<FeatureController> log,
            IWebHostEnvironment env) : base(
            db, log, env)
        {
        }


        [HttpGet("GetFeatures")]
        public async Task<ActionResult<ICollection<FeatureDTO>>> GetFeatures([FromQuery] int mainFeatureId)
        {

            var mainFeature = await db.MainFeatures.Where(mf => mf.Id == mainFeatureId)
                .Include(mf => mf.TestClasses)
                .ThenInclude(c => c.Tests)
                .ThenInclude(t => t.Features).FirstOrDefaultAsync();


            if (mainFeature == null)
                return NotFound("Could not find the main feature");


            var featuresDtos = new List<FeatureDTO>();

            foreach (var testClass in mainFeature.TestClasses)
            {
                foreach (var test in testClass.Tests)
                {
                    foreach (var feature in test.Features)
                    {
                        if (featuresDtos.Any(fdto => fdto.FeatureName == feature.FeatureName))
                            continue;

                        featuresDtos.Add(new FeatureDTO(feature));
                    }
                }

            }

            // var features = await db.Features.ToListAsync();
            // var featuresDtos = features.Select(feature => new FeatureDTO(feature)).ToList();

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
            [FromQuery] int versionId,
            [FromQuery] int? mainFeatureId)
        {

            List<Feature> features;

            if (mainFeatureId is null or 0)
            {
                features = await db.Features
                    .Where(f => f.FirstVersionId <= versionId)
                    .Include(f => f.Tests)
                    .ToListAsync();
            }
            else
            {
                var mainFeatures = await db.MainFeatures
                    .Where(mf => mf.Id == mainFeatureId)
                    .Include(mf => mf.TestClasses)
                    .ThenInclude(c => c.Tests)
                    .ThenInclude(t => t.Features)
                    .ToListAsync();

                features = new List<Feature>();
                foreach (var mainFeature in mainFeatures)
                {
                    foreach (var testClass in mainFeature.TestClasses)
                    {
                        foreach (var test in testClass.Tests)
                        {
                            foreach (var feature in test.Features.Where(feature => !features.Contains(feature)))
                            {
                                features.Add(feature);
                            }
                        }
                    }
                }
            }

            var executionsInVersion = await db.Executions
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
                    if (featureTest.FirstVersionId <= versionId)
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
                        else if (lastExInFeature.ExecutionResult != ExecutionResult.Skipped &&
                                 lastExInFeature.ExecutionResult != ExecutionResult.UnExecuted)
                        {
                            totalExecutedTests++;
                        }
                    }

                }

                if (totalTests == 0)
                    continue;

                var featureSummary = new FeatureSummary(feature, totalTests, totalExecutedTests, totalPassedTests);

                featureSummaries.Add(featureSummary);
            }

            return Ok(featureSummaries);
        }
    }
}