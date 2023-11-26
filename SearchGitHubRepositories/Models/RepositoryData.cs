namespace SearchGitHubRepositories.Models
{
    public class Owner
    {
        public string login { get; set; }
    }
    public class Item
    {
        public string name { get; set; }
        public string full_name { get; set; }
        public Owner owner { get; set; }
        public int stargazers { get; set; }
        public int watchers { get; set; }
    }
    public class RepositoryData
    {
        public List<Item> items { get; set; }
    }
}
