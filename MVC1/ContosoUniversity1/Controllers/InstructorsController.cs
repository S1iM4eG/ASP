
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity1.Models;
using ContosoUniversity1.Models.ViewModels;

public class InstructorsController : Controller
{
    private readonly ContosoUniversity1Context _context;

    public InstructorsController(ContosoUniversity1Context context)
    {
        _context = context;
    }

    // GET: INSTRUCTORS
    //public async Task<IActionResult> Index()
    //{
    //    return View(await _context.Instructors.ToListAsync());
    //}
    public async Task<IActionResult> Index(int? id, int? courseID)
    {
        InstructorIndexData viewModel = new InstructorIndexData();
        viewModel.Instructors = await _context
            .Instructors
            .Include(i => i.OfficeAssignment)
            .Include(i => i.CourseAssignments)
                .ThenInclude(ca => ca.Course)
                    .ThenInclude(c => c.Enrollments)
                        .ThenInclude(e => e.Student)
            .Include(i => i.CourseAssignments)
                .ThenInclude(ca => ca.Course)
                    .ThenInclude(c => c.Department)
            .AsNoTracking()
            .OrderBy(i => i.LastName)
            .ToListAsync();

        if (id != null)
        {
            ViewData["InstructorID"] = id.Value;
            /*Instructor instructor = viewModel
                .Instructors
                .Where(i => i.ID == id.Value).Single();
            viewModel.Courses = instructor
                .CourseAssignments
                .Select(ca => ca.Course);
            */

            viewModel.Courses = viewModel
                .Instructors.Where(i => i.ID == id.Value).Single()
                .CourseAssignments.Select(ca => ca.Course);
        }

        if (courseID != null)
        {
            ViewData["CourseID"] = courseID.Value;
            viewModel.Enrollments = viewModel
                .Courses.Where(c => c.CourseID == courseID.Value)
                .Single()
                .Enrollments;
        }

        return View(viewModel);
    }

    // GET: INSTRUCTORS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructors
            .FirstOrDefaultAsync(m => m.ID == id);
        if (instructor == null)
        {
            return NotFound();
        }

        return View(instructor);
    }

    // GET: INSTRUCTORS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: INSTRUCTORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,HireDate,FullName,OfficeAssignment,CourseAssignments")] Instructor instructor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(instructor);
    }

    // GET: INSTRUCTORS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        Instructor instructor = await _context.Instructors
            .Include(i => i.OfficeAssignment)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
        if (instructor == null)
        {
            return NotFound();
        }
        return View(instructor);
    }

    // POST: INSTRUCTORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(int? id, [Bind("ID,LastName,FirstName,HireDate")] Instructor instructor)
    {
        if (id == null) return NotFound();
        instructor = await _context.Instructors
            .Include(i => i.OfficeAssignment)
            .FirstOrDefaultAsync(i => i.ID == id);

        bool ok = await TryUpdateModelAsync<Instructor>
            (
                instructor,
                "",
                i => i.FirstName, i => i.LastName, i => i.HireDate, i => i.OfficeAssignment
            );
        if (ok)
        {
            if (String.IsNullOrWhiteSpace(instructor.OfficeAssignment?.Location))
                instructor.OfficeAssignment = null;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(instructor);
    }

    //public async Task<IActionResult> Edit(int? id, [Bind("ID,LastName,FirstName,HireDate,FullName,OfficeAssignment,CourseAssignments")] Instructor instructor)
    //{
    //    if (id != instructor.ID)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(instructor);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!InstructorExists(instructor.ID))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(instructor);
    //}

    // GET: INSTRUCTORS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructors
            .FirstOrDefaultAsync(m => m.ID == id);
        if (instructor == null)
        {
            return NotFound();
        }

        return View(instructor);
    }

    // POST: INSTRUCTORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor != null)
        {
            _context.Instructors.Remove(instructor);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InstructorExists(int? id)
    {
        return _context.Instructors.Any(e => e.ID == id);
    }
}
