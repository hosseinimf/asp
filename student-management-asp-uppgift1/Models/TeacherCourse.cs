using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_asp_uppgift1.Models
{
    public class TeacherCourse
    {
       


        public string ApplicationUserId { get; set; }

        public int CourseId { get; set; }


        public virtual ApplicationUser Teacher { get; set; }

        public virtual Course Course { get; set; }

        //ok
    }
}
