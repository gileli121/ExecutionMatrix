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

            if (body.TestClass.MainFeatures.Count == 0)
                return BadRequest("No main features specified for the test class");

            var testClass = await db.TestClasses.Where(c =>
                    c.PackageName == body.TestClass.PackageName && c.ClassName == body.TestClass.ClassName)
                .Include(c => c.MainFeatures)
                .FirstOrDefaultAsync();

            var version = await db.Versions.Where(v => v.Name == body.TestClass.VersionName).FirstOrDefaultAsync();
            if (version == null)
            {
                version = new Database.Tables.Version(body.TestClass.VersionName);
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
                var tests = await db.Tests.Where(t =>
                        (t.TestClass == null || (t.TestClass.ClassName == body.TestClass.ClassName &&
                                                 t.TestClass.PackageName == body.TestClass.PackageName)))
                    .ToListAsync();

                test = tests.FirstOrDefault(testCheck =>
                    testCheck.TestClass != null && testCheck.IsMatchToExecution(body));
                if (test == null)
                {
                    // Create the test here and add it to the DB
                    test = new Test(body, testClass, version);
                    db.Tests.Add(test);
                }
            }

            var allMainFeatures = await db.MainFeatures.Include(mf => mf.TestClasses).ToListAsync();

            foreach (var mainFeatureName in body.TestClass.MainFeatures)
            {
                var mainFeature = allMainFeatures.FirstOrDefault(f => f.FeatureName == mainFeatureName);
                if (mainFeature != null)
                {
                    if (!mainFeature.TestClasses.Contains(testClass))
                        mainFeature.TestClasses.Add(testClass);
                }
                else
                {
                    mainFeature = new MainFeature(mainFeatureName, version);
                    mainFeature.TestClasses.Add(testClass);
                    db.MainFeatures.Add(mainFeature);
                }
            }


            // if (body.FeatureNames == null || body.FeatureNames.Count == 0)
            // return BadRequest("The FeatureNames field is required.");

            if (body.Features?.Count > 0)
            {
                foreach (var bodyFeatureName in body.Features)
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
                        body.Features.Any(bodyFeatureName => test.Features[i].FeatureName == bodyFeatureName);

                    if (featureFound)
                        i++;
                    else
                        test.Features.RemoveAt(i);
                }

            }
            else if (test.Features?.Count > 0)
            {
                test.Features.Clear();
            }


            var execution = new Execution(test, body, version);
            db.Executions.Add(execution);


            await db.SaveChangesAsync();

            return Ok();
        }
    }
}