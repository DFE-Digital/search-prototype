using OpenQA.Selenium;

namespace Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Drivers;

public interface IWebDriverFactory
{
    Lazy<IWebDriver> CreateDriver();
}
