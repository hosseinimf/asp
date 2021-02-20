using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_asp_uppgift1.Models
{
    public class Course
    {
        [DisplayName("Course Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Course name")]
        public string CourseName { get; set; }
        
        public string TeacherId { get; set; }



        public virtual ApplicationUser Teacher { get; set; }

        public virtual List<StudentCourse> StudentCourses { get; set; }

        //ok
    }
}
