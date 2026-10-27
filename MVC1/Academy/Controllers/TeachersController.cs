
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academy.Models;
using Academy.Data;

public class TeachersController : Controller
{
    private readonly AcademyContext _context;

    public TeachersController(AcademyContext context)
    {
        _context = context;
    }

    // GET: TEACHERSS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Teachers.ToListAsync());
    }

    // GET: TEACHERSS/Details/5
    public async Task<IActionResult> Details(short? teacher_id)
    {
        if (teacher_id == null)
        {
            return NotFound();
        }

        var teachers = await _context.Teachers
            .FirstOrDefaultAsync(m => m.teacher_id == teacher_id);
        if (teachers == null)
        {
            return NotFound();
        }

        return View(teachers);
    }

    // GET: TEACHERSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TEACHERSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("teacher_id,last_name,first_name,middle_name,birth_date,email,phone,photo,work_since,rate")] Teachers teachers)
    {
        if (ModelState.IsValid)
        {
            _context.Add(teachers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(teachers);
    }

    // GET: TEACHERSS/Edit/5
    public async Task<IActionResult> Edit(short? teacher_id)
    {
        if (teacher_id == null)
        {
            return NotFound();
        }

        var teachers = await _context.Teachers.FindAsync(teacher_id);
        if (teachers == null)
        {
            return NotFound();
        }
        return View(teachers);
    }

    // POST: TEACHERSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(short? teacher_id, [Bind("teacher_id,last_name,first_name,middle_name,birth_date,email,phone,photo,work_since,rate")] Teachers teachers)
    {
        if (teacher_id != teachers.teacher_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(teachers);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeachersExists(teachers.teacher_id))
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
        return View(teachers);
    }

    // GET: TEACHERSS/Delete/5
    public async Task<IActionResult> Delete(short? teacher_id)
    {
        if (teacher_id == null)
        {
            return NotFound();
        }

        var teachers = await _context.Teachers
            .FirstOrDefaultAsync(m => m.teacher_id == teacher_id);
        if (teachers == null)
        {
            return NotFound();
        }

        return View(teachers);
    }

    // POST: TEACHERSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(short? teacher_id)
    {
        var teachers = await _context.Teachers.FindAsync(teacher_id);
        if (teachers != null)
        {
            _context.Teachers.Remove(teachers);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TeachersExists(short? teacher_id)
    {
        return _context.Teachers.Any(e => e.teacher_id == teacher_id);
    }
}
