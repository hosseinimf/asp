using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using student_management_asp_uppgift1.Data;
using student_management_asp_uppgift1.Models;

namespace student_management_asp_uppgift1.Controllers
{


    [Authorize]
    public class StudentCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentCoursesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: StudentCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Study.Include(s => s.Course).Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentCourses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.Study
                .Include(s => s.Course)
                .Include(s => s.Student)
                .Include(s => s.Course.Teacher)
                .FirstOrDefaultAsync(m => m.ApplicationUserId == id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            return View(studentCourse);
        }

        // GET: StudentCourses/Create
        public async Task<IActionResult> Create()
        {
            var students = await _userManager.GetUsersInRoleAsync("Student");

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseName");
            ViewData["StudentId"] = new SelectList(students, "Id", "DisplayName");
            return View();
        }

        // POST: StudentCourses/Create
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
            //ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", studentCourse.CourseId);
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", studentCourse.ApplicationUserId);
            return View(studentCourse);
        }




        // GET: StudentCourses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.Study
                .Include(s => s.Course)
                .Include(s => s.Student)
                .Include(s => s.Course.Teacher)
                .FirstOrDefaultAsync(m => m.ApplicationUserId == id);

            if (studentCourse == null)
            {
                return NotFound();
            }
            
            ViewData["CoursesList"] = new SelectList(_context.Courses, "Id", "CourseName", "Select a Course");
            ViewData["CurrentStudent"] = studentCourse.Student.DisplayName.ToString();


            return View(studentCourse);
        }

        // POST: StudentCourses/Edit/5
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

        // GET: StudentCourses/Delete/5
        public async Task<IActionResult> Delete(int courseId, string studentId)
        {
            if (courseId == 0 || studentId == null)
            {
                return NotFound();
            }

            var studentCourse1 = await _context.Study.SingleOrDefaultAsync(kk => kk.CourseId == courseId && kk.ApplicationUserId == studentId);

            if(studentCourse1 == null)
            {
                return NotFound();
            }

            
            return View(studentCourse1);
        }

        // POST: StudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int courseId, string studentId)
        {
            var studentCourse = await _context.Study.FindAsync(courseId, studentId);
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
