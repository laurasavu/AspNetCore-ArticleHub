using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DTO;

[Route("api/[controller]")]
[ApiController]
public class WriterController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public WriterController(AppDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    // GET: api/Writer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WriterDTO>>> GetWriters()
    {
        var writers = await _context.Writers.Include(w => w.Articles).ThenInclude(a => a.Comments).ToListAsync();
        return Ok(_mapper.Map<List<WriterDTO>>(writers));
    }

    // GET: api/Writer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WriterDTO>> GetWriter(long id)
    {
        var writer = await _context.Writers
            .Include(w => w.Articles)
                .ThenInclude(a => a.Comments)
           
            .FirstOrDefaultAsync(w => w.Id == id);

        if (writer == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<WriterDTO>(writer));
    }

    // POST: api/Writer
    [HttpPost]
    public async Task<ActionResult<WriterDTO>> PostWriter(WriterDTO writerDTO)
    {
        var writer = _mapper.Map<Writer>(writerDTO);
        _context.Writers.Add(writer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWriter), new { id = writer.Id }, _mapper.Map<WriterDTO>(writer));
    }

    // PUT: api/Writer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWriter(long id, WriterDTO writerDTO)
    {
        if (id != writerDTO.Id)
        {
            return BadRequest();
        }

        var writer = await _context.Writers.FindAsync(id);
        if (writer == null)
        {
            return NotFound();
        }

        _mapper.Map(writerDTO, writer);

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
