using DataAnnotationsExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_asp_uppgift1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        public string LastName { get; set; }


        public byte[] ProfilePicture { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";



        public virtual List<StudentCourse> StudentCourses { get; set; }

        public virtual List<TeacherCourse> TeacherCourses { get; set; }


        //ok

    }
}
