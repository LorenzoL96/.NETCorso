using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCorso.Models.ViewModels;

namespace NETCorso.Models.Services.Application
{
    public interface ICourseService
    {
        List<CourseViewModel> GetCourses();
        CourseDetailViewModel GetCourse(int id);
    }
}