using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthentificationIntro.Models;
using AuthentificationIntro.Data;
using Microsoft.AspNetCore.Identity;

namespace AuthentificationIntro.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; //Dont forget this!!

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            this._context = context; // added this 
            _userManager = um; 

        }

        public IActionResult Index()
        {
            return View(this._context.TodoModel.ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
