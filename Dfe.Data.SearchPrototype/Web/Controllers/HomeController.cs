using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.Web.Models;
using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dfe.Data.SearchPrototype.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUseCase<SearchByKeywordRequest, SearchByKeywordResponse> _searchByKeywordUseCase;
        private readonly IMapper<SearchByKeywordResponse, SearchResultsViewModel> _mapper;

        public HomeController(
            ILogger<HomeController> logger, 
            IUseCase<SearchByKeywordRequest, SearchByKeywordResponse> searchByKeywordUseCase,
            IMapper<SearchByKeywordResponse, SearchResultsViewModel> mapper
            )
        {
            _logger = logger;
            _searchByKeywordUseCase = searchByKeywordUseCase;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string searchKeyWord)
        {
            if (string.IsNullOrEmpty(searchKeyWord))
            {
                return View();
            }
            ViewBag.SearchQuery = searchKeyWord;

            SearchByKeywordResponse response =
                await _searchByKeywordUseCase.HandleRequest(
                    new SearchByKeywordRequest(searchKeyWord, "establishments"));

            SearchResultsViewModel viewModel = _mapper.MapFrom(response);
            return View(viewModel);
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
