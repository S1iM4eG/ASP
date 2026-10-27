
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academy.Models;
using Academy.Data;

public class DisciplinesController : Controller
{
    private readonly AcademyContext _context;

    public DisciplinesController(AcademyContext context)
    {
        _context = context;
    }

    // GET: DISCIPLINESS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Disciplines.ToListAsync());
    }

    // GET: DISCIPLINESS/Details/5
    public async Task<IActionResult> Details(int? discipline_id)
    {
        if (discipline_id == null)
        {
            return NotFound();
        }

        var disciplines = await _context.Disciplines
            .FirstOrDefaultAsync(m => m.discipline_id == discipline_id);
        if (disciplines == null)
        {
            return NotFound();
        }

        return View(disciplines);
    }

    // GET: DISCIPLINESS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DISCIPLINESS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("discipline_id,discipline_name,number_of_lessons")] Disciplines disciplines)
    {
        if (ModelState.IsValid)
        {
            _context.Add(disciplines);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(disciplines);
    }

    // GET: DISCIPLINESS/Edit/5
    public async Task<IActionResult> Edit(int? discipline_id)
    {
        if (discipline_id == null)
        {
            return NotFound();
        }

        var disciplines = await _context.Disciplines.FindAsync(discipline_id);
        if (disciplines == null)
        {
            return NotFound();
        }
        return View(disciplines);
    }

    // POST: DISCIPLINESS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? discipline_id, [Bind("discipline_id,discipline_name,number_of_lessons")] Disciplines disciplines)
    {
        if (discipline_id != disciplines.discipline_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(disciplines);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplinesExists(disciplines.discipline_id))
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
        return View(disciplines);
    }

    // GET: DISCIPLINESS/Delete/5
    public async Task<IActionResult> Delete(int? discipline_id)
    {
        if (discipline_id == null)
        {
            return NotFound();
        }

        var disciplines = await _context.Disciplines
            .FirstOrDefaultAsync(m => m.discipline_id == discipline_id);
        if (disciplines == null)
        {
            return NotFound();
        }

        return View(disciplines);
    }

    // POST: DISCIPLINESS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? discipline_id)
    {
        var disciplines = await _context.Disciplines.FindAsync(discipline_id);
        if (disciplines != null)
        {
            _context.Disciplines.Remove(disciplines);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DisciplinesExists(int? discipline_id)
    {
        return _context.Disciplines.Any(e => e.discipline_id == discipline_id);
    }
}
