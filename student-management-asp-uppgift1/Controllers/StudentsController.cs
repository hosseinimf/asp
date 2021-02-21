using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using student_management_asp_uppgift1.Data;
using student_management_asp_uppgift1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_asp_uppgift1.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public StudentsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        //Get: Students
        public async Task<IActionResult> Index()
        {
            
            var studentsList = await _userManager.GetUsersInRoleAsync("Student");
            return View(studentsList);
            //return View(await _context.Users.ToListAsync());
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        public IActionResult Create()
        {
            var coursesContext = _context.Courses;
            ViewData["StudentCourses"] = new SelectList(coursesContext, "Id", "Id");
            return View();
        }


        //post: Student/Create
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser model)
        {
            ApplicationUser student;

            if (ModelState.IsValid)
            {
                student = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                await _userManager.CreateAsync(student, "Student123!");
                await _userManager.AddToRoleAsync(student, "Student");
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        // GET: Student/Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Users
                .FindAsync(id);


            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            System.Diagnostics.Debug.WriteLine("testtt***************--------////////////////////////////\n\n" + id + model.FirstName + "\n //////////////");


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(model.Id))
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
            return View(model);
        }





        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }


        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentStudent = await _userManager.FindByIdAsync(id);
            ViewData["CurrentStudent"] = currentStudent.DisplayName;

            var student = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var teacher = await _context.Users.FindAsync(id);
            _context.Users.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //////////////////////////
    }
}
