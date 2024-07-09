using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace UnitTestProject1
{
    [Binding]
    public class SpecFlowHooks
    {
        private readonly IObjectContainer _container;
        public SpecFlowHooks(IObjectContainer container)
        {
            _container = container;
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

        [BeforeScenario]
        public void CreateWebDriver()
        {
            // Create and configure a concrete instance of IWebDriver
            IWebDriver driver = new ChromeDriver();
            {

            };

            // Make this instance available to all other step definitions
            _container.RegisterInstanceAs(driver);
        }

        [AfterScenario]
        public void DestroyWebDriver()
        {
            IWebDriver driver = _container.Resolve<IWebDriver>();

            driver.Close();
            driver.Dispose();
        }

        [After]
        public void After()
        {
            var processes = Process.GetProcessesByName("Dfe.Data.SearchPrototype.Web");
            processes[0].Kill();
        }
    }
}