using SearchGitHubRepositories.Data;
using SearchGitHubRepositories.Models;

namespace SearchGitHubRepositories.Implementation
{
    public class SearchEngine:ISearchEngine
    {
        SearchContext _db;

        public SearchEngine(SearchContext db)
        {
            this._db = db;
        }

        public string GetResult(string searchStr)
        {
            return SearchRepositoryRequest(searchStr);
        }

        private string SearchRepositoryRequest(string searchStr)
        {
            if (searchStr == null)
            {
                return null;
            }

            searchStr = searchStr.ToLower();
           
            if (SearchInDb(searchStr, out var res))
            {
                return res;
            }

            return SearchInGithub(searchStr);
        }

        private bool SearchInDb(string searchStr, out string res)
        {
            Search searchRes = _db.Search.Where(i => String.Compare(i.SearchString, searchStr) == 0).FirstOrDefault();

            if (Equals(searchRes, null) == false)
            {
                res = searchRes.ResultData;
                return true;
            }

            res = null;
            return false;
        }

        private string SearchInGithub(string searchStr)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            string response = client.GetStringAsync($"https://api.github.com/search/repositories?q={searchStr}").Result;

            //Сохраняем результат поиска в БД
            Search newSearch = new Search()
            {
                SearchString = searchStr,
                ResultData = response,
            };

            _db.Search.Add(newSearch);
            _db.SaveChanges();

            return response;
        }
    }
}
