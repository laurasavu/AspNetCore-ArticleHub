
using System.ComponentModel.DataAnnotations;

public class Writer
{
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;


    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
  //  public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
  
}
