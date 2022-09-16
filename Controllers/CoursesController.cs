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
        
        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        //Responsabile di presentare l'elenco dei corsi
        public IActionResult Index()
        {
            ViewData["Title"] = "Catalogo dei corsi";
            List<CourseViewModel> courses = courseService.GetCourses();
            return View(courses);

        }

        public IActionResult Detail(int id){
            CourseDetailViewModel viewModel = courseService.GetCourse(id);
            ViewData["Title"] = viewModel.Title; //viene passata automaticamente alla view
            return View(viewModel);
        }
        
    }
}