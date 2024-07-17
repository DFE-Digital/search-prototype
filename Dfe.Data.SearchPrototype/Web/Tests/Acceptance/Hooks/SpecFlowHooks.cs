using BoDi;
using System.Diagnostics;
using TechTalk.SpecFlow;
using Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Drivers;
using Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Options;
using Microsoft.Extensions.Options;

namespace UnitTestProject1
{
    [Binding]
    public class SpecFlowHooks
    {
        public SpecFlowHooks( )
        {
        }

        [Before]
        public void Before()
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "Dfe.Data.SearchPrototype.Web.exe",
                }
            };
            process.Start();
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
            var webProcesses = Process.GetProcessesByName("Dfe.Data.SearchPrototype.Web");
            webProcesses[0].Kill();
        }
    }
}