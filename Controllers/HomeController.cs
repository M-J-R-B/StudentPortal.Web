using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using System.Diagnostics;

namespace StudentPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ApplicationDbContext dbContext;
        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
           
            
                var students = await dbContext.Students.ToListAsync();

                return View(students);
            
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
