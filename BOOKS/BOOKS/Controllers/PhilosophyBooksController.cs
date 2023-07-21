using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOOKS.Data;
using BOOKS.Models;

namespace BOOKS.Controllers
{
    public class PhilosophyBooksController : Controller
    {
        private readonly ApplicationDbCon _context;

        public PhilosophyBooksController(ApplicationDbCon context)
        {
            _context = context;
        }

        // GET: PhilosophyBooks
        public async Task<IActionResult> Index()
        {
              return _context.PhilosophyBooks != null ? 
                          View(await _context.PhilosophyBooks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbCon.PhilosophyBooks'  is null.");
        }

        // GET: PhilosophyBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhilosophyBooks == null)
            {
                return NotFound();
            }

            var philosophyBooks = await _context.PhilosophyBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (philosophyBooks == null)
            {
                return NotFound();
            }

            return View(philosophyBooks);
        }

        // GET: PhilosophyBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhilosophyBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookName,AuthorName,CreatedDateTime")] PhilosophyBooks philosophyBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(philosophyBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(philosophyBooks);
        }

        // GET: PhilosophyBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhilosophyBooks == null)
            {
                return NotFound();
            }

            var philosophyBooks = await _context.PhilosophyBooks.FindAsync(id);
            if (philosophyBooks == null)
            {
                return NotFound();
            }
            return View(philosophyBooks);
        }

        // POST: PhilosophyBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,AuthorName,CreatedDateTime")] PhilosophyBooks philosophyBooks)
        {
            if (id != philosophyBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(philosophyBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhilosophyBooksExists(philosophyBooks.Id))
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
            return View(philosophyBooks);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhilosophyBooks == null)
            {
                return NotFound();
            }

            var philosophyBooks = await _context.PhilosophyBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (philosophyBooks == null)
            {
                return NotFound();
            }

            return View(philosophyBooks);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhilosophyBooks == null)
            {
                return Problem("Entity set 'ApplicationDbCon.PhilosophyBooks'  is null.");
            }
            var philosophyBooks = await _context.PhilosophyBooks.FindAsync(id);
            if (philosophyBooks != null)
            {
                _context.PhilosophyBooks.Remove(philosophyBooks);
            }
            
            await _context.SaveChangesAsync();
            TempData["keyMessage5"] = "Book deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool PhilosophyBooksExists(int id)
        {
          return (_context.PhilosophyBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
