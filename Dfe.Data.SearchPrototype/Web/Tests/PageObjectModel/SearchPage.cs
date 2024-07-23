using Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Drivers;
using OpenQA.Selenium;

namespace Dfe.Data.SearchPrototype.Web.Tests.PageObjectModel;

public sealed class SearchPage : BasePage
{
    public SearchPage(IWebDriverContext driverContext) : base(driverContext)
    {   
    }

    public IWebElement Heading => DriverContext.Wait.UntilElementExists(By.CssSelector("header div div:nth-of-type(2) a"));
}
