using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DTO;
using Project.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ArticleController(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticles()
        {
            var articles = await _context.Articles
                .Include(a => a.Writer)
                .ToListAsync();

            return _mapper.Map<List<ArticleDTO>>(articles);
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDTO>> GetArticle(long id)
        {
            var article = await _context.Articles
                .Include(a => a.Writer)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return _mapper.Map<ArticleDTO>(article);
        }

        // POST: api/Article
        [HttpPost]
        public async Task<ActionResult<ArticleDTO>> PostArticle(ArticleDTO articleDto)
        {
            var article = _mapper.Map<Article>(articleDto);

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            articleDto.Id = article.Id;
            return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, articleDto);
        }

        // PUT: api/Article/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(long id, ArticleDTO articleDto)
        {
            if (id != articleDto.Id)
            {
                return BadRequest();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _mapper.Map(articleDto, article);

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Article/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(long id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(long id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }

        // GET: api/Article/Titles
        [HttpGet("Titles")]
        public async Task<ActionResult<IEnumerable<string>>> GetArticleTitles()
        {
            var titles = await _context.Articles
                .Select(a => a.Title)
                .ToListAsync();
            return Ok(titles);
        }

        // GET: api/Article/TitlesAndWriters
        [HttpGet("TitlesAndWriters")]
        public async Task<ActionResult<IEnumerable<object>>> GetArticleTitlesAndWriters()
        {
            var TitlesAndWriters = await _context.Articles
                .Include(a => a.Writer)
                .Select(a => new
                {
                    ArticleId = a.Id,
                    Title = a.Title,
                    WriterName = a.Writer != null ? a.Writer.Name : "Unknown",
                    WriterId = a.WriterId
                })
                .ToListAsync();

            return Ok(TitlesAndWriters);
        }
    }
}