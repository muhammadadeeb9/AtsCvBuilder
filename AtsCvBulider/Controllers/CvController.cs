using AtsCvBuilder.Data;
using AtsCvBuilder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtsCvBuilder.Controllers;

// Controllers/CvController.cs
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CvController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CvController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: api/cv
    [HttpGet]
    public async Task<IActionResult> GetAllCVs()
    {
        var userId = _userManager.GetUserId(User);
        var cvs = await _context.CVs
            .Include(c => c.Sections)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return Ok(cvs);
    }
    // GET: api/cv/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCVById(int id)
    {
        var userId = _userManager.GetUserId(User);

        var cv = await _context.CVs
            .Include(c => c.Sections) //  ÷„Ì‰ «·√ﬁ”«„ «·„— »ÿ… »«·‹ CV
            .Where(c => c.UserId == userId && c.Id == id) // «· √ﬂœ „‰ √‰ «·‹ CV ··„” Œœ„ «·Õ«·Ì
            .FirstOrDefaultAsync();

        if (cv == null)
        {
            return NotFound(new { Message = "CV not found or does not belong to the current user." });
        }

        return Ok(cv);
    }
    // POST: api/cv
    [HttpPost]
    public async Task<IActionResult> CreateCV([FromBody] CvDto model)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);
        var cv = new CV
        {
            Title = model.Title,
            Summary = model.Summary,
            UserId = userId
        };

        _context.CVs.Add(cv);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "CV created successfully.", CV = cv });
    }

    // PUT: api/cv/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCV(int id, [FromBody] CvDto model)
    {
        var userId = _userManager.GetUserId(User);
        var cv = await _context.CVs.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (cv == null)
            return NotFound(new { Message = "CV not found." });

        cv.Title = model.Title;
        cv.Summary = model.Summary;

        await _context.SaveChangesAsync();

        return Ok(new { Message = "CV updated successfully.", CV = cv });
    }

    // DELETE: api/cv/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCV(int id)
    {
        var userId = _userManager.GetUserId(User);
        var cv = await _context.CVs.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (cv == null)
            return NotFound(new { Message = "CV not found." });

        _context.CVs.Remove(cv);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "CV deleted successfully." });
    }

    // POST: api/cv/{cvId}/section
    [HttpPost("{cvId}/section")]
    public async Task<IActionResult> AddSection(int cvId, [FromBody] SectionDto dto)
    {
        var userId = _userManager.GetUserId(User);

        //  √ﬂœ √‰ «·”Ì—… «·–« Ì… „ÊÃÊœ… Ê··„” Œœ„ «·Õ«·Ì
        var cv = await _context.CVs.FirstOrDefaultAsync(c => c.Id == cvId && c.UserId == userId);
        if (cv == null)
            return NotFound(new { Message = "CV not found." });

        var section = new CvSection
        {
            SectionType = dto.SectionType,
            Content = dto.Content,
            CVId = cvId
        };

        _context.CvSections.Add(section);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Section added successfully.", Section = section });
    }
    // PUT: api/cv/section/{id}
    [HttpPut("section/{id}")]
    public async Task<IActionResult> UpdateSection(int id, [FromBody] SectionDto dto)
    {
        var userId = _userManager.GetUserId(User);

        var section = await _context.CvSections
            .Include(s => s.CV)
            .FirstOrDefaultAsync(s => s.Id == id && s.CV.UserId == userId);

        if (section == null)
            return NotFound(new { Message = "Section not found." });

        section.SectionType = dto.SectionType;
        section.Content = dto.Content;

        await _context.SaveChangesAsync();

        return Ok(new { Message = "Section updated successfully.", Section = section });
    }

    // DELETE: api/cv/section/{id}
    [HttpDelete("section/{id}")]
    public async Task<IActionResult> DeleteSection(int id)
    {
        var userId = _userManager.GetUserId(User);

        var section = await _context.CvSections
            .Include(s => s.CV)
            .FirstOrDefaultAsync(s => s.Id == id && s.CV.UserId == userId);

        if (section == null)
            return NotFound(new { Message = "Section not found." });

        _context.CvSections.Remove(section);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Section deleted successfully." });
    }
}