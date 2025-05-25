using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DTO;
using Project.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CommentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments()
        {
            var comments = await _context.Comments
                .Include(c => c.Writer)
                .Include(c => c.Article)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<CommentDTO>>(comments));
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(long id)
        {
            var comment = await _context.Comments
                .Include(c => c.Writer)
                .Include(c => c.Article)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommentDTO>(comment));
        }

        // POST: api/Comment
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> PostComment(CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var createdCommentDto = _mapper.Map<CommentDTO>(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, createdCommentDto);
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(long id, CommentDTO commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _mapper.Map(commentDto, comment);

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(long id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(long id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }

        [HttpGet("Article/{articleId}/Comments/Content")]
        public async Task<ActionResult<IEnumerable<string>>> GetCommentContentsByArticleId(long articleId)
        {
            var commentContents = await _context.Comments
                .Where(c => c.ArticleId == articleId)
                .Select(c => c.Content)
                .ToListAsync();

            if (commentContents == null || !commentContents.Any())
            {
                return NotFound(new { Message = $"No comments found for ArticleId {articleId}" });
            }

            return Ok(commentContents);
        }
    }
}
