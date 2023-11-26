namespace SearchGitHubRepositories.Implementation
{
    public interface ISearchEngine
    {
        string GetResult(string searchStr);
    }
}