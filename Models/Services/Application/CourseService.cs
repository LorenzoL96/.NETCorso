using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCorso.Models.Enums;
using NETCorso.Models.ValueTypes;
using NETCorso.Models.ViewModels;

namespace NETCorso.Models.Services.Application
{
    public class CourseService : ICourseService
    {
        public List<CourseViewModel> GetCourses()
        {
            List<CourseViewModel> courseList = new List<CourseViewModel>();
            var rand = new Random();
            for (int i = 1; i <= 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var course = new CourseViewModel
                {
                    Id = i,
                    Title = $"Corso {i}",
                    CurrentPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                    Author = "Nome Cognome",
                    Rating = rand.NextDouble() * 5.0,
                    ImagePath = "~logo/jpeg"
                };
                courseList.Add(course);
            }
            return courseList;
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            var rand = new Random();
            var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
            var course = new CourseDetailViewModel
            {
                Id = id,
                Title = $"Corso {id}",
                CurrentPrice = new Money(Currency.EUR, price),
                FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                Author = "Nome Cognome",
                Rating = rand.Next(10, 50) / 10.0,
                ImagePath = "~logo/jpeg",
                Description = $"Descrizione {id}",
                Lesson = new List<LessonViewModel>()
            };

            for(var i = 1; i <= 5; i++){
                var lessons = new LessonViewModel {
                    Title = $"Lezione  {i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40, 90))

                };
                course.Lesson.Add(lessons);
            }
            return course;
        }
    }
}