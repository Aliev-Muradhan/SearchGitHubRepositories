using Microsoft.EntityFrameworkCore;
using SearchGitHubRepositories.Models;

namespace SearchGitHubRepositories.Data
{
    public class SearchContext:DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Search> Search { get; set; }
    }
}
