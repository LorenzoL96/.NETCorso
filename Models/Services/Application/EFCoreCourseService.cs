using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NETCorso.Models.ViewModels;
using prova.Models.Services.Infrastructure;

namespace NETCorso.Models.Services.Application
{
    public class EFCoreCourseService : ICourseService
    {
        private readonly MyCourseDbContext dbContext;

        public EFCoreCourseService(MyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {

            CourseDetailViewModel viewModel = await dbContext.Courses
            .AsNoTracking() //MIGLIORA LE PRESTAZIONI
            .Where(course => course.Id == id).Select(course => 
            new CourseDetailViewModel {
                Id = course.Id,
                Title = course.Title,
                ImagePath = course.ImagePath,
                Author = course.Author,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice,
                Description = course.Description,
                Lessons = course.Lessons.Select(lesson => new LessonViewModel {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    Duration = lesson.Duration
                }).ToList()

            })
            .SingleAsync(); //restituisce il primo elemento di un elenco
            
            return viewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            List<CourseViewModel> courses = await dbContext.Courses
            .AsNoTracking() //MIGLIORA LE PRESTAZIONI
            .Select(course => 
            new CourseViewModel {
                Id = course.Id,
                Title = course.Title,
                ImagePath = course.ImagePath,
                Author = course.Author,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice
            }).ToListAsync();
            
            return courses;
        }
    }
}