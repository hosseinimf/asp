using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_asp_uppgift1.Models
{
    public class StudentCourse
    {

        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public int CourseId { get; set; }

        public string CompositeId => $"{ApplicationUserId}{CourseId}";

        public virtual ApplicationUser Student { get; set; }

        public virtual Course Course { get; set; }


        //ok
    }
}
