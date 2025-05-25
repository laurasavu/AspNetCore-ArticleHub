using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Project.DTO;
using Project.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Writer> Writers { get; set; }
    public DbSet<Comment> Comments { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurarea relațiilor
        modelBuilder.Entity<Writer>()
            .HasMany(w => w.Articles)
            .WithOne(a => a.Writer)
            .HasForeignKey(a => a.WriterId);

        modelBuilder.Entity<Article>()
            .HasMany(a => a.Comments)
            .WithOne(c => c.Article)
            .HasForeignKey(c => c.ArticleId);

        // Adăugarea datelor de test
        modelBuilder.Entity<Writer>().HasData(
            new Writer { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
            new Writer { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
        );

        modelBuilder.Entity<Article>().HasData(
            new Article { Id = 1, Title = "Introduction to C#", Content = "This is a beginner's guide to C# programming.", WriterId = 1 },
            new Article { Id = 2, Title = "Advanced Entity Framework", Content = "This article covers advanced EF Core topics.", WriterId = 2 }
        );

        modelBuilder.Entity<Comment>().HasData(
            new Comment { Id = 1, Content = "Great article!", WriterId = 2, ArticleId = 1 },
            new Comment { Id = 2, Content = "Very informative, thanks!", WriterId = 1, ArticleId = 2 }
        );
    }
}
