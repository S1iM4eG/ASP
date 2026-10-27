
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academy.Models;
using Academy.Data;

public class StudentsController : Controller
{
    private readonly AcademyContext _context;

    public StudentsController(AcademyContext context)
    {
        _context = context;
    }

    // GET: STUDENTSS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Students.ToListAsync());
    }

    // GET: STUDENTSS/Details/5
    public async Task<IActionResult> Details(int? stud_id)
    {
        if (stud_id == null)
        {
            return NotFound();
        }

        var students = await _context.Students
            .FirstOrDefaultAsync(m => m.stud_id == stud_id);
        if (students == null)
        {
            return NotFound();
        }

        return View(students);
    }

    // GET: STUDENTSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: STUDENTSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("stud_id,last_name,first_name,middle_name,birth_date,email,phone,photo,group")] Students students)
    {
        if (ModelState.IsValid)
        {
            _context.Add(students);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(students);
    }

    // GET: STUDENTSS/Edit/5
    public async Task<IActionResult> Edit(int? stud_id)
    {
        if (stud_id == null)
        {
            return NotFound();
        }

        var students = await _context.Students.FindAsync(stud_id);
        if (students == null)
        {
            return NotFound();
        }
        return View(students);
    }

    // POST: STUDENTSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? stud_id, [Bind("stud_id,last_name,first_name,middle_name,birth_date,email,phone,photo,group")] Students students)
    {
        if (stud_id != students.stud_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(students);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(students.stud_id))
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
        return View(students);
    }

    // GET: STUDENTSS/Delete/5
    public async Task<IActionResult> Delete(int? stud_id)
    {
        if (stud_id == null)
        {
            return NotFound();
        }

        var students = await _context.Students
            .FirstOrDefaultAsync(m => m.stud_id == stud_id);
        if (students == null)
        {
            return NotFound();
        }

        return View(students);
    }

    // POST: STUDENTSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? stud_id)
    {
        var students = await _context.Students.FindAsync(stud_id);
        if (students != null)
        {
            _context.Students.Remove(students);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudentsExists(int? stud_id)
    {
        return _context.Students.Any(e => e.stud_id == stud_id);
    }
}
