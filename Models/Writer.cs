
using System.ComponentModel.DataAnnotations;

public class Writer
{
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    // Navigation properties
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
  
}
