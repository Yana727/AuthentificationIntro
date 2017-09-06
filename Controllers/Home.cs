using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace AuthentificationIntro.Controllers
{
    public class Home : Controller
    {
        private readonly Authdb _context;

        public Home(Authdb context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            var authdb = _context.ToDoModel.Include(t => t.User);
            return View(await authdb.ToListAsync());
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDoModel
                .Include(t => t.User)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Complete,Time,ID")] ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", toDoModel.UserId);
            return View(toDoModel);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDoModel.SingleOrDefaultAsync(m => m.ID == id);
            if (toDoModel == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", toDoModel.UserId);
            return View(toDoModel);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Complete,Time,ID")] ToDoModel toDoModel)
        {
            if (id != toDoModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoModelExists(toDoModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", toDoModel.UserId);
            return View(toDoModel);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDoModel
                .Include(t => t.User)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoModel = await _context.ToDoModel.SingleOrDefaultAsync(m => m.ID == id);
            _context.ToDoModel.Remove(toDoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoModelExists(int id)
        {
            return _context.ToDoModel.Any(e => e.ID == id);
        }
    }
}
