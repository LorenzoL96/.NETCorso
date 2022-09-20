using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCorso.Models.Options;
using NETCorso.Models.Services.Application;
using NETCorso.Models.Services.Infrastructure;
using prova.Models.Services.Infrastructure;

namespace prova
{
    public class Startup
    {
        public IConfiguration Configuration { get;}

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
                
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            //services.AddTransient<ICourseService, EFCoreCourseService>();       //Costruisce automaticamente l'oggetto CourseService e lo passa alle classi che necessitano dell'oggetto
            services.AddTransient<ICourseService, AdoNetCourseService>();       //Costruisce automaticamente l'oggetto CourseService e lo passa alle classi che necessitano dell'oggetto
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>(); //Costruisce automaticamente l'oggetto CourseService e lo passa alle classi che necessitano dell'oggetto 
            //services.AddScoped<MyCourseDbContext>();
            //services.AddDbContext<MyCourseDbContext>();
            services.AddDbContextPool<MyCourseDbContext>(optionsBuilder => {
                string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString);
            });

            //Options: contiene i riferimenti per i file di configurazione
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<CoursesOptions>(Configuration.GetSection("Courses"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else{
                app.UseExceptionHandler("/Error"); //middleware per dare un messaggio sensato all'utente nel caso in cui l'aplicazione
                //vada in eccezione in ambiente Production. Verrà gestita da ErrorController che ha come action "Index"
            }

            app.UseStaticFiles(); //middleware per gestione file statici (immagini, html, css....)

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routeBuilder => {
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
