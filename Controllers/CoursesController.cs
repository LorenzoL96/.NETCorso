using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NETCorso.Models.Services.Application;
using NETCorso.Models.ViewModels;

namespace NETCorso.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        
        public CoursesController(ICachedCourseService courseService)
        {
            this.courseService = courseService;
        }

        //Responsabile di presentare l'elenco dei corsi
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Catalogo dei corsi";
            List<CourseViewModel> courses = await courseService.GetCoursesAsync();
            return View(courses);

        }

        public async Task<IActionResult> Detail(int id){
            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);
            ViewData["Title"] = viewModel.Title; //viene passata automaticamente alla view
            return View(viewModel);
        }
        
    }
}