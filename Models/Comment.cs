using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; } = string.Empty;

        public long WriterId { get; set; }
        public long ArticleId { get; set; }


        [ForeignKey("WriterId")]
        public virtual Writer Writer { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}