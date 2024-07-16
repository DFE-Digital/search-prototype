using Dfe.Data.SearchPrototype.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dfe.Data.SearchPrototype.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchResults(string searchKeyWord)
        {
            if (string.IsNullOrEmpty(searchKeyWord))
            {
                return View("Index");
            }
            ViewBag.SearchQuery = searchKeyWord;

            var searchItems = new SearchResultsViewModel();
            return View(searchItems);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
