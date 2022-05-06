#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LesBlaguesDeFlavio.Data;
using LesBlaguesDeFlavio.Models;
using Microsoft.AspNetCore.Authorization;

namespace LesBlaguesDeFlavio.Controllers
{
    public class BlaguesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlaguesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Blagues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blague.ToListAsync());
        }

        // GET: Blagues/SearchForm
        public async Task<IActionResult> SearchForm()
        {
            return View();
        }

        // POST: Blagues/SearchResults
        public async Task<IActionResult> SearchResults(string MotsCles)
        {
            return View("Index", await _context.Blague.Where(b => b.Question.Contains(MotsCles)).ToListAsync());
        }

        // GET: Blagues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blague = await _context.Blague
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blague == null)
            {
                return NotFound();
            }

            return View(blague);
        }

        // GET: Blagues/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blagues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Reponse")] Blague blague)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blague);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blague);
        }

        // GET: Blagues/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blague = await _context.Blague.FindAsync(id);
            if (blague == null)
            {
                return NotFound();
            }
            return View(blague);
        }

        // POST: Blagues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Reponse")] Blague blague)
        {
            if (id != blague.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blague);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlagueExists(blague.Id))
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
            return View(blague);
        }

        // GET: Blagues/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blague = await _context.Blague
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blague == null)
            {
                return NotFound();
            }

            return View(blague);
        }

        // POST: Blagues/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blague = await _context.Blague.FindAsync(id);
            _context.Blague.Remove(blague);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlagueExists(int id)
        {
            return _context.Blague.Any(e => e.Id == id);
        }
    }
}
