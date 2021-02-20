using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_asp_uppgift1.Enums
{
    public enum Roles
    {
        Admin,
        Teacher,
        Student
    }
}


////@foreach (var student in Model)
//{
//            < dl class= "row" >
 

//                 < dt class= "col-sm-2" >
//                      @Html.DisplayNameFor(student => student.Student)
//                  </ dt >
  
//                  < dd class= "col-sm-10" >
//                       @Html.DisplayFor(modelstudent => student.Student.DisplayName)
//                   </ dd >
   
//                   < dt class= "col-sm-2" >
//                        @Html.DisplayNameFor(model => model.Course.CourseName)
//                    </ dt >
    
//                    < dd class= "col-sm-10" >
//                         @Html.DisplayFor(modelStudent => student.Course.CourseName)
//                     </ dd >
     
//                     < dt class= "col-sm-2" >
//                          @Html.DisplayNameFor(model => model.Course.Teacher)
//                      </ dt >
      
//                      < dd class= "col-sm-10" >
//                           @Html.DisplayFor(modelStudent => student.Course.Teacher.DisplayName)
//                       </ dd >
