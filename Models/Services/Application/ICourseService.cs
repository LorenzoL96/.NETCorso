using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCorso.Models.ViewModels;

namespace NETCorso.Models.Services.Application
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCoursesAsync();
        Task<CourseDetailViewModel> GetCourseAsync(int id);
    }
}