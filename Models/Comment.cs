using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    [Key]
    public long Id { get; set; }

    [Required]
    [StringLength(500)]
    public string Content { get; set; }

    public long WriterId { get; set; }
    public long ArticleId { get; set; }

    // Navigation properties
    [ForeignKey("WriterId")]
    public virtual Writer Writer { get; set; }

    [ForeignKey("ArticleId")]
    public virtual Article Article { get; set; }
}
