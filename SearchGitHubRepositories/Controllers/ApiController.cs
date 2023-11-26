using Microsoft.AspNetCore.Mvc;
using SearchGitHubRepositories.Data;
using SearchGitHubRepositories.Implementation;
using SearchGitHubRepositories.Models;
using System.Text.Json;

namespace SearchGitHubRepositories.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : Controller
    {
        ISearchEngine _se;
        SearchContext _db;
        public ApiController(ISearchEngine se, SearchContext db)
        {
            _db = db;
            _se = se;
        }

        [Route("find")]
        [HttpPost]
        public IActionResult FindPost()
        {
            if (Request.Form.ContainsKey("search") == false) return Json(null);

            string search = Request.Form["search"];
            return Json(_se.GetResult(search));
        }

        [Route("find")]
        [HttpGet]
        public IActionResult FindGet()
        {
            if (Request.Form.ContainsKey("search") == false) return Json(null);

            string search = Request.Form["search"];

            RepositoryData data = JsonSerializer.Deserialize<RepositoryData>(_se.GetResult(search));
            return Json(data);
        }

        [Route("find/{id}")]
        [HttpDelete]
        public IActionResult FindDelete(int id)
        {
            Search search = _db.Search.Where(s => s.Id == id).FirstOrDefault();
            if (search != null)
            {
                _db.Search.Remove(search);
                _db.SaveChanges();

                return new NoContentResult();
            }
            
            return new NotFoundResult();
        }
    }
}
