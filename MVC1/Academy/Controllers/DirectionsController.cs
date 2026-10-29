
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academy.Models;
using Academy.Data;
using Academy.Models.ViewModels;

public class DirectionsController : Controller
{
    private readonly AcademyContext _context;

    public DirectionsController(AcademyContext context)
    {
        _context = context;
    }

    // GET: DIRECTIONSS
    public async Task<IActionResult> Index(int? id)
    {
        
        var viewModel = new DirectionIndexData();
        viewModel.Directions = await _context.Directions
            .AsNoTracking()
            .ToListAsync();

        if (id != null)
        {
            ViewData["DirectionID"] = id.Value;
            // подгрузите связанные данные для выбранного направления,
            // например:
             viewModel.Groups = await _context.Groups
                 .Where(g => g.direction == id.Value)
                 .AsNoTracking()
                 .ToListAsync();
        }

        return View(viewModel);
        //return View(await _context.Directions.ToListAsync());
    }

    // GET: DIRECTIONSS/Details/5
    public async Task<IActionResult> Details(int? direction_id)
    {
        if (direction_id == null)
        {
            return NotFound();
        }

        var directions = await _context.Directions
            .FirstOrDefaultAsync(m => m.direction_id == direction_id);
        if (directions == null)
        {
            return NotFound();
        }

        return View(directions);
    }

    // GET: DIRECTIONSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DIRECTIONSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("direction_id,direction_name")] Directions directions)
    {
        if (ModelState.IsValid)
        {
            _context.Add(directions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(directions);
    }

    // GET: DIRECTIONSS/Edit/5
    public async Task<IActionResult> Edit(int? direction_id)
    {
        if (direction_id == null)
        {
            return NotFound();
        }

        var directions = await _context.Directions.FindAsync(direction_id);
        if (directions == null)
        {
            return NotFound();
        }
        return View(directions);
    }

    // POST: DIRECTIONSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? direction_id, [Bind("direction_id,direction_name")] Directions directions)
    {
        if (direction_id != directions.direction_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(directions);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectionsExists(directions.direction_id))
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
        return View(directions);
    }

    // GET: DIRECTIONSS/Delete/5
    public async Task<IActionResult> Delete(int? direction_id)
    {
        if (direction_id == null)
        {
            return NotFound();
        }

        var directions = await _context.Directions
            .FirstOrDefaultAsync(m => m.direction_id == direction_id);
        if (directions == null)
        {
            return NotFound();
        }

        return View(directions);
    }

    // POST: DIRECTIONSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? direction_id)
    {
        var directions = await _context.Directions.FindAsync(direction_id);
        if (directions != null)
        {
            _context.Directions.Remove(directions);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DirectionsExists(int? direction_id)
    {
        return _context.Directions.Any(e => e.direction_id == direction_id);
    }
}
