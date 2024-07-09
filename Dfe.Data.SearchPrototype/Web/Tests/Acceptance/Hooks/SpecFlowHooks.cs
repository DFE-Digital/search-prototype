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
                    FileName = "dotnet",
                    Arguments = "run --urls=http://localhost:7001",
                    UseShellExecute = true,
                    WorkingDirectory = "C:\\Users\\aoakes1\\source\\repos\\DFE-Digital\\search-prototype\\dfe.data.SearchPrototype\\Web"
                }
            };
            process.Start();
            Thread.Sleep(1000);
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
            var processes = Process.GetProcessesByName("WindowsTerminal");
            processes[0].CloseMainWindow();
        }
    }
}