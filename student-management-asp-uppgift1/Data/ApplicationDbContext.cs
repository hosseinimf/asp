using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using student_management_asp_uppgift1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using student_management_asp_uppgift1.ViewModels;

namespace student_management_asp_uppgift1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<TeacherCourse> Teaches { get; set; }
        public DbSet<StudentCourse> Study { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentCourse>().HasKey(s => new { s.ApplicationUserId, s.CourseId });
            builder.Entity<TeacherCourse>().HasKey(t => new { t.ApplicationUserId, t.CourseId });
        }




        public DbSet<student_management_asp_uppgift1.ViewModels.TeacherViewModel> TeacherViewModel { get; set; }




        


    }
}