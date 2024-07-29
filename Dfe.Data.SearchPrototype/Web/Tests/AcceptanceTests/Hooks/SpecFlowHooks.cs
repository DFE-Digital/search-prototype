using BoDi;
using System.Diagnostics;
using TechTalk.SpecFlow;
using Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Drivers;
using Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Options;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;
using Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Extensions;

namespace UnitTestProject1
{
    [Binding]
    public class SpecFlowHooks
    {
        private readonly ITestOutputHelper _logger;

        public SpecFlowHooks(ITestOutputHelper logger)
        {
            _logger = logger;
        }

        [BeforeTestRun]
        public static void BeforeTest(ObjectContainer container)
        {
            //var newPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\"));
            //
            //var process = new Process
            //{
            //    StartInfo =
            //    {
            //        //FileName = "Dfe.Data.SearchPrototype.Web.exe",
            //        WorkingDirectory = newPath,
            //        FileName = "dotnet",
            //        Arguments = "run --urls=http://localhost:5000"
            //    }
            //};
            //process.Start();
            //Thread.Sleep(1000);

            container.BaseContainer.RegisterInstanceAs(OptionsHelper.GetOptions<WebOptions>(WebOptions.Key));

            var driverOptions = OptionsHelper.GetOptions<WebDriverOptions>(WebDriverOptions.Key);
            if (string.IsNullOrEmpty(driverOptions.Value.DriverBinaryDirectory))
            {
                driverOptions.Value.DriverBinaryDirectory = Directory.GetCurrentDirectory();
            }
            container.BaseContainer.RegisterInstanceAs<IOptions<WebDriverOptions>>(driverOptions);
            var accessibilityOptions = OptionsHelper.GetOptions<AccessibilityOptions>(AccessibilityOptions.Key);
            accessibilityOptions.Value.CreateArtifactOutputDirectory();
            container.BaseContainer.RegisterInstanceAs(accessibilityOptions);
        }

        [BeforeScenario]
        public void CreateWebDriver(IObjectContainer container)
        {
            container.RegisterTypeAs<WebDriverFactory, IWebDriverFactory>();
            container.RegisterTypeAs<WebDriverContext, IWebDriverContext>();
        }

        [AfterScenario]
        public void After(
        FeatureContext featureContext,
        ScenarioContext scenarioContext,
        ITestOutputHelper logger,
        IOptions<WebDriverOptions> driverOptions,
        WebDriverSessionOptions sessionOptions,
        IWebDriverContext driverContext
    )
        {
            using (driverContext)
            {
                logger.WriteLine($"START {nameof(After)}");
                if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
                {
                    logger.WriteLine($"FAILURE DETECTED: {scenarioContext.TestError.Message}");
                    logger.WriteLine($"FAILURE URL: {driverContext.Driver.Url}");
                    logger.WriteLine($"FAILURE HTML: {driverContext.Driver.PageSource}");
                    var featureName = featureContext.FeatureInfo.Title.ToLowerRemoveHyphens();
                    var scenarioName = scenarioContext.ScenarioInfo.Title.ToLowerRemoveHyphens();
                    var testName = $"{sessionOptions.Device}-{featureName}-{scenarioName}";
                    driverContext.TakeScreenshot(logger, testName);
                }
                logger.WriteLine($"FINISH {nameof(After)}");
            }
        }
        
        [AfterTestRun]
        public static void After()
        {
            //var webProcesses = Process.GetProcessesByName("Dfe.Data.SearchPrototype.Web");
            //webProcesses[0].Kill();
        }

    }
}