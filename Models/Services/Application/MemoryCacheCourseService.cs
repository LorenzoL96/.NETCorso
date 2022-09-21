using Microsoft.Extensions.Caching.Memory;
using NETCorso.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETCorso.Models.Services.Application
{
    public class MemoryCacheCourseService : ICachedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;
        public MemoryCacheCourseService(ICourseService courseService, IMemoryCache memoryCache) //IMemoryCache per ottenere gli oggetti dalla cache
        {
            this.courseService = courseService;
            this.memoryCache = memoryCache;
        }

        //TODO: ricordati di usare memoryCache.Remove($"Course{id}") quando aggiorni il corso

        Task<CourseDetailViewModel> ICourseService.GetCourseAsync(int id)
        {
    
             return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry => 
            {
                //cacheEntry.SetSize(1); //Da usare se si è impostato un limite di cache
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60)); //Esercizio: provate a recuperare il valore 60 usando il servizio di configurazione
                return courseService.GetCourseAsync(id);
            });
        }

        Task<List<CourseViewModel>> ICourseService.GetCoursesAsync()
        {
             return memoryCache.GetOrCreateAsync($"Courses", cacheEntry => 
            {
                //cacheEntry.SetSize(1); //Da usare se si è impostato un limite di cache
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.GetCoursesAsync();
            });
        }
    }
}