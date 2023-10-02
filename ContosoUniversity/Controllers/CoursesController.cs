using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;
        public CoursesController(SchoolContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Department)
                .ToListAsync();
            var courseViewModels = new List<CourseViewModel>();
            foreach (var course in courses)
            {
                var courseViewModel = new CourseViewModel
                {
                    course = course,
                    assignedInstructors = await _context.CourseAssignments
                        .Where(ca => ca.CourseID == course.CourseID)
                        .Select(ca => ca.Instructor)
                        .ToListAsync(),
                    assignedStudents = await _context.Enrollments
                        .Where(ca => ca.CourseID == course.CourseID)
                        .Select(ca => ca.Student)
                        .ToListAsync(),
                };
                courseViewModels.Add(courseViewModel);
            }

            // Pass the list of CourseViewModels to the view
            return View(courseViewModels);
        }

        // get details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        // get create
        public IActionResult Create()
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FullName");
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            return View();
        }


        // post create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Title,Credits,DepartmentID")] Course course)
        {
            ModelState.Remove("Department");
            ModelState.Remove("Enrollments");
            ModelState.Remove("CourseAssignment");

            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FullName", course.DepartmentID);
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name", course.DepartmentID);
            return View(course);
        }


        // get edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }

            // Populate the department dropdown
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name", course.DepartmentID);

            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID,Title,Credits,DepartmentID")] Course course)
        {
            if (id != course.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // If ModelState is not valid, redisplay the form with validation errors
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name", course.DepartmentID);
            return View(course);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
        }

        // get delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // post delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int courseId)
        {
            Course course = await _context.Courses
                .SingleAsync(c => c.CourseID == courseId);

            //remove any associated course assignments
            var assignments = await _context.CourseAssignments
            .Where(ca => ca.CourseID == courseId)
            .ToListAsync();

            foreach (var assignment in assignments)
            {
                _context.CourseAssignments.Remove(assignment);
            }

            //remove enrollments
            var enrollments = await _context.Enrollments
                .Where(e => e.CourseID == courseId)
                .ToListAsync();

            foreach (var enrollment in enrollments)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();


            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
