using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCorso.Models.ViewModels
{
    public class CourseDetailViewModel: CourseViewModel
    {
        public string Description { get; set; }
        public List<LessonViewModel> Lesson { get; set; }
        public TimeSpan TotalCourseDuration{
            get => TimeSpan.FromSeconds(Lesson?.Sum(l => l.Duration.TotalSeconds) ?? 0);
        }
        
    }
}