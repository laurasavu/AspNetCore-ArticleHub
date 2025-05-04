using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly AppDbContext _context;

    public CommentController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Comment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        return await _context.Comments
            .Include(c => c.Writer)
            .Include(c => c.Article)
            .ToListAsync();
    }

    // GET: api/Comment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(long id)
    {
        var comment = await _context.Comments
            .Include(c => c.Writer)
            .Include(c => c.Article)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return NotFound();
        }

        return comment;
    }

    // POST: api/Comment
    [HttpPost]
    public async Task<ActionResult<Comment>> PostComment(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
    }

    // PUT: api/Comment/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutComment(long id, Comment comment)
    {
        if (id != comment.Id)
        {
            return BadRequest();
        }

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
}