
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academy.Models;
using Academy.Data;

public class GroupsController : Controller
{
    private readonly AcademyContext _context;

    public GroupsController(AcademyContext context)
    {
        _context = context;
    }

    // GET: GROUPSS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Groups.ToListAsync());
    }

    // GET: GROUPSS/Details/5
    public async Task<IActionResult> Details(int? group_id)
    {
        if (group_id == null)
        {
            return NotFound();
        }

        var groups = await _context.Groups
            .FirstOrDefaultAsync(m => m.group_id == group_id);
        if (groups == null)
        {
            return NotFound();
        }

        return View(groups);
    }

    // GET: GROUPSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GROUPSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("group_id,group_name,direction,start_date,start_time,learning_days")] Groups groups)
    {
        if (ModelState.IsValid)
        {
            _context.Add(groups);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(groups);
    }

    // GET: GROUPSS/Edit/5
    public async Task<IActionResult> Edit(int? group_id)
    {
        if (group_id == null)
        {
            return NotFound();
        }

        var groups = await _context.Groups.FindAsync(group_id);
        if (groups == null)
        {
            return NotFound();
        }
        return View(groups);
    }

    // POST: GROUPSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? group_id, [Bind("group_id,group_name,direction,start_date,start_time,learning_days")] Groups groups)
    {
        if (group_id != groups.group_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(groups);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupsExists(groups.group_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(groups);
    }

    // GET: GROUPSS/Delete/5
    public async Task<IActionResult> Delete(int? group_id)
    {
        if (group_id == null)
        {
            return NotFound();
        }

        var groups = await _context.Groups
            .FirstOrDefaultAsync(m => m.group_id == group_id);
        if (groups == null)
        {
            return NotFound();
        }

        return View(groups);
    }

    // POST: GROUPSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? group_id)
    {
        var groups = await _context.Groups.FindAsync(group_id);
        if (groups != null)
        {
            _context.Groups.Remove(groups);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GroupsExists(int? group_id)
    {
        return _context.Groups.Any(e => e.group_id == group_id);
    }
}
