using Microsoft.AspNetCore.Mvc;
using SearchGitHubRepositories.Implementation;
using SearchGitHubRepositories.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SearchGitHubRepositories.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ISearchEngine _se;
        public HomeController(ILogger<HomeController> logger, ISearchEngine se)
        {
            _se = se;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string? search)
        {
            if (String.IsNullOrEmpty(search) == true)
                return View();

            string json = _se.GetResult(search);
            RepositoryData data = JsonSerializer.Deserialize<RepositoryData>(json);
            ViewBag.SearchString = search;

            return View(data);
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