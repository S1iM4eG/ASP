
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity1.Models;
using ContosoUniversity1.Models.ViewModels;

namespace ContosoUniversity1.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly ContosoUniversity1Context _context;

        public InstructorsController(ContosoUniversity1Context context)
        {
            _context = context;
        }

        // GET: Instructors
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Instructors.ToListAsync());
        }*/
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
					.Select(ca => ca.Course);*/

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

        // GET: Instructors/Details/5
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

        // GET: Instructors/Create
        public IActionResult Create()
        {
            Instructor instructor = new Instructor();
            instructor.CourseAssignments = new List<CourseAssignment>();
            PopulateAssignedCourseData(instructor);
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,HireDate")] Instructor instructor, string[] selectedCourses)
        {
            if (selectedCourses != null) UpdateInstructorCourses(selectedCourses, instructor);
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Instructor instructor = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(ca => ca.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (instructor == null) return NotFound();
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditPost(int? id)
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,HireDate")] Instructor instructor, string[] selectedCourses)
        {
            if (id == null) return NotFound();

            instructor = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(ca => ca.Course)
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
                UpdateInstructorCourses(selectedCourses, instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            UpdateInstructorCourses(selectedCourses, instructor);
            PopulateAssignedCourseData(instructor);

            return View(instructor);
        }

        void PopulateAssignedCourseData(Instructor instructor)
        {
            IQueryable<Course> allCourses = _context.Courses;
            HashSet<int> instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.CourseID));
            List<AssignedCourseData> viewModel = new List<AssignedCourseData>();
            foreach (Course course in allCourses)
            {
                viewModel.Add
                (
                    new AssignedCourseData
                    {
                        CourseID = course.CourseID,
                        Title = course.Title,
                        Assigned = instructorCourses.Contains(course.CourseID)
                    });
            }
            ViewData["Courses"] = viewModel;
        }
        void UpdateInstructorCourses(string[] selectedCourses, Instructor instructor)
        {
            //if (selectedCourses == null)
            //{
            //    instructor.CourseAssignments = new List<CourseAssignment>();
            //    return;
            //}

           // HashSet<string> selectedCoursesHS = new HashSet<string>(selectedCourses);
            List<CourseAssignment> courseAssignments = new List<CourseAssignment>();
            foreach(string courseID in selectedCourses)
            {
                courseAssignments.Add
                (
                    new CourseAssignment
                    {
                        InstructorID = instructor.ID,
                        CourseID = Convert.ToInt32(courseID)
                    }
                );
            }
            instructor.CourseAssignments = courseAssignments;
            //HashSet<int> instructorCourses = new HashSet<int>
            //    (instructor.CourseAssignments.Select(c => c.Course.CourseID));
            //foreach (Course course in _context.Courses)
            //{
            //    if (selectedCoursesHS.Contains(course.CourseID.ToString()))
            //    {
            //        if (!instructorCourses.Contains(course.CourseID))
            //        {
            //            instructor.CourseAssignments.Add(new CourseAssignment
            //            {
            //                InstructorID = instructor.ID,
            //                CourseID = course.CourseID
            //            });
            //        }
            //    }
            //    else
            //    {
            //        if (instructorCourses.Contains(course.CourseID))
            //        {
            //            CourseAssignment courseToRemove = instructor
            //                .CourseAssignments
            //                .FirstOrDefault(i => i.CourseID == course.CourseID);
            //            _context.Remove(courseToRemove);
            //        }
            //    }
            //}
        }

        /*public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,HireDate")] Instructor instructor)
        {
            if (id != instructor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.ID))
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
            return View(instructor);
        }*/

        // GET: Instructors/Delete/5
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

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}