using student_management_asp_uppgift1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_asp_uppgift1.ViewModels
{
    public class EditCoursesViewModel
    {
        public Course CurrentCourse { get; set; }
        public ApplicationUser Teacher { get; set; }
    }
}
