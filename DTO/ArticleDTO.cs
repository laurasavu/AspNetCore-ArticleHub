using Project.Models;

namespace Project.DTO
{
    public class ArticleDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long WriterId { get; set; }
        public List<string> Comments { get; set; }
    }
}