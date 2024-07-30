using BoDi;
using System.Diagnostics;
using TechTalk.SpecFlow;
using Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Drivers;
using Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Options;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;

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

        [Before]
        public void Before()
        {
            //var workingDir = Directory.GetCurrentDirectory();
            //_logger.WriteLine(workingDir);
            //
            //
            //var process = new Process
            //{
            //    StartInfo =
            //    {
            //        //FileName = "Dfe.Data.SearchPrototype.Web.exe",
            //        WorkingDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\")),
            //        FileName = "dotnet",
            //        Arguments = "run --urls=http://localhost:5000"
            //    }
            //};
            //process.Start();
            //Thread.Sleep(1000);
        }

        [BeforeTestRun]
        public static void BeforeTest(ObjectContainer container)
        {

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


        [After]
        public void After()
        {
            //var webProcesses = Process.GetProcessesByName("Dfe.Data.SearchPrototype.Web");
            //webProcesses[0].Kill();
        }
    }
}