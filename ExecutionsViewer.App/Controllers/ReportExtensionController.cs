using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
    public class ReportExtensionController : AppControllerBase<ReportExtensionController>
    {
        public ReportExtensionController(ExecutionsViewerDbContext db, ILogger<ReportExtensionController> log,
            IWebHostEnvironment env) : base(
            db, log, env)
        {
        }


        [HttpPost("SubmitExecution")]
        public async Task<ActionResult> PostExecution(PostExecutionDTO body)
        {
            // Create the test class if not exists


            // Create the test in the test class if not exists
            var tests = db.Tests
                .Include(t => t.Features)
                .Include(t => t.ChildTests)
                .Include(t => t.TestOwner)
                .AsEnumerable()
                .Where(t => t.TestId == null && t.TestMethodName == body.TestMethodName)
                .ToList();


            var testClass = await db.TestClasses.Where(c =>
                    c.PackageName == body.TestClass.PackageName && c.ClassName == body.TestClass.ClassName)
                .FirstOrDefaultAsync();

            var version = await db.Versions.Where(v => v.Name == body.VersionName).FirstOrDefaultAsync();
            if (version == null)
            {
                version = new Database.Tables.Version(body.VersionName);
                db.Versions.Add(version);
            }

            Test test;
            if (testClass == null)
            {
                testClass = new TestClass(body.TestClass);
                // Create the test here and add it to the DB
                test = new Test(body, testClass, version);
                db.Tests.Add(test);
            }
            else
            {
                test = tests.FirstOrDefault(testCheck =>
                    testCheck.TestClass != null && testCheck.IsMatchToExecution(body));
                if (test == null)
                {
                    // Create the test here and add it to the DB
                    test = new Test(body, testClass, version);
                    db.Tests.Add(test);
                }
            }


            if (body.FeatureNames == null || body.FeatureNames.Count == 0)
                return BadRequest("The FeatureNames field is required.");

            foreach (var bodyFeatureName in body.FeatureNames)
            {
                var feature = await db.Features.Where(f => f.FeatureName == bodyFeatureName).Include(f => f.Tests)
                    .FirstOrDefaultAsync();
                if (feature == null)
                {
                    feature = new Feature(bodyFeatureName, version);
                    feature.Tests.Add(test);
                    db.Features.Add(feature);
                }
                else
                {
                    var featureFoundInTest = test.Features.Any(testFeature => testFeature.Id == feature.Id);
                    if (!featureFoundInTest)
                        feature.Tests.Add(test);
                }
            }

            var i = 0;
            while (i < test.Features.Count)
            {
                var featureFound =
                    body.FeatureNames.Any(bodyFeatureName => test.Features[i].FeatureName == bodyFeatureName);

                if (featureFound)
                    i++;
                else
                    test.Features.RemoveAt(i);
            }


            var execution = new Execution(test, body, version);
            db.Executions.Add(execution);


            await db.SaveChangesAsync();

            return Ok();
        }
    }
}