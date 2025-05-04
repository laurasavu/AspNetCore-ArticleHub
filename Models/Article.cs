using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Article
{
    [Key]
    public long Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    
    public long WriterId { get; set; }

  
    [ForeignKey("WriterId")]
    public virtual Writer Writer { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
