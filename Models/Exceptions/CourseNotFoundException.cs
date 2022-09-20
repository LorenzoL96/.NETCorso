using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCorso.Models.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException(int courseId) : base(message: $"Course {courseId} not found"){
        
        }

    }
}