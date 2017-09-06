using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AuthentificationIntro.Data;
using AuthentificationIntro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuthentificationIntro.Models;

namespace AuthentificationIntro.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; //Dont forget this!!

        public ToDoController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            this._context = context; // added this 
            _userManager = um; 

        }
        public IActionResult Index()
        {
           return View(_context.TodoModel.ToList());
        }
        [HttpPost] // talks to html 
        public IActionResult Index(string Name)
        {
            var currentToDo = new ToDoModel
            {
                TaskName = Name
            };
            _context.Add(currentToDo); //adds to the database 
            _context.SaveChanges(); //adds to the database 
            return View(_context.TodoModel.ToList());
        }
        [HttpPost]
        public IActionResult Complete(int ID)
        {
            var fin = _context.TodoModel.FirstOrDefault(m => m.ID == ID);
            fin.Finished();
            _context.SaveChanges();
            return Redirect("Index");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
