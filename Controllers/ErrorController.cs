using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NETCorso.Models.Exceptions;

namespace NETCorso.Controllers
{
    public class ErrorController: Controller
    {

        public IActionResult index(){
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            switch(feature.Error){
                case CourseNotFoundException exc:
                    ViewData["Title"] = "Corso non trovato";
                    Response.StatusCode = 404;
                    return View("CourseNotFound");

                default:
                    ViewData["Title"] = "Errore";
                    return View();
            }
        }
        
    }
}