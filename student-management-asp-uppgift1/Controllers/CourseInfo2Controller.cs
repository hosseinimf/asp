using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using student_management_asp_uppgift1.Data;
using student_management_asp_uppgift1.Models;

namespace student_management_asp_uppgift1.Controllers
{
    public class CourseInfo2Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public CourseInfo2Controller(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CourseInfo2
        public async Task<IActionResult> Index()
        {
            var courses = _context.Courses.Include(s => s.StudentCourses);


            var applicationDbContext = _context.Study.Include(s => s.Course).Include(s => s.Student);
            return View(await courses.ToListAsync());
        }




        // GET: CourseInfo2/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            
            IEnumerable<StudentCourse> studentCourses = _context.Study
                .Include(s => s.Course)
                .Include(s => s.Student)
                .Include(s => s.Course.Teacher)
                .Where(m => m.CourseId == id);

            var teacherId = _context.Courses.Find(id).TeacherId;
            ViewBag.TeacherOf = await _userManager.FindByIdAsync(teacherId);

            if (studentCourses == null)
            {
                return NotFound();
            }

            return View(studentCourses);
        }




        // GET: CourseInfo2/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: CourseInfo2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationUserId,CourseId")] StudentCourse studentCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", studentCourse.CourseId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", studentCourse.ApplicationUserId);
            return View(studentCourse);
        }

        // GET: CourseInfo2/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.Study.FindAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", studentCourse.CourseId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", studentCourse.ApplicationUserId);
            return View(studentCourse);
        }

        // POST: CourseInfo2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ApplicationUserId,CourseId")] StudentCourse studentCourse)
        {
            if (id != studentCourse.ApplicationUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentCourseExists(studentCourse.ApplicationUserId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", studentCourse.CourseId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", studentCourse.ApplicationUserId);
            return View(studentCourse);
        }

        // GET: CourseInfo2/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.Study
                .Include(s => s.Course)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.ApplicationUserId == id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            return View(studentCourse);
        }

        // POST: CourseInfo2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var studentCourse = await _context.Study.FindAsync(id);
            _context.Study.Remove(studentCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentCourseExists(string id)
        {
            return _context.Study.Any(e => e.ApplicationUserId == id);
        }
    }
}
