using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class WriterController : ControllerBase
{
    private readonly AppDbContext _context;

    public WriterController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Writer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Writer>>> GetWriters()
    {
        return await _context.Writers.Include(w => w.Articles).Include(w => w.Comments).ToListAsync();
    }

    // GET: api/Writer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Writer>> GetWriter(long id)
    {
        var writer = await _context.Writers
            .Include(w => w.Articles)
            .Include(w => w.Comments)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (writer == null)
        {
            return NotFound();
        }

        return writer;
    }

    // POST: api/Writer
    [HttpPost]
    public async Task<ActionResult<Writer>> PostWriter(Writer writer)
    {
        _context.Writers.Add(writer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWriter), new { id = writer.Id }, writer);
    }

    // PUT: api/Writer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWriter(long id, Writer writer)
    {
        if (id != writer.Id)
        {
            return BadRequest();
        }

        _context.Entry(writer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WriterExists(id))
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

    // DELETE: api/Writer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWriter(long id)
    {
        var writer = await _context.Writers.FindAsync(id);
        if (writer == null)
        {
            return NotFound();
        }

        _context.Writers.Remove(writer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WriterExists(long id)
    {
        return _context.Writers.Any(e => e.Id == id);
    }
}