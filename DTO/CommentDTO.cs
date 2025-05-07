namespace Project.DTO
{
    public class CommentDTO
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string WriterName { get; set; }
        public string ArticleTitle { get; set; }
        public long WriterId { get; set; }
        public long ArticleId { get; set; }
    }
}