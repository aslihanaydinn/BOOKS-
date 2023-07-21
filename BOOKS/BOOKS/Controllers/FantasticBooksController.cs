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
    public class FantasticBooksController : Controller
    {
        private readonly ApplicationDbCon _context;

        public FantasticBooksController(ApplicationDbCon context)
        {
            _context = context;
        }

        // GET: FantasticBooks
        public async Task<IActionResult> Index()
        {
              return _context.FantasticBooks != null ? 
                          View(await _context.FantasticBooks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbCon.FantasticBooks'  is null.");
        }

        // GET: FantasticBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FantasticBooks == null)
            {
                return NotFound();
            }

            var fantasticBooks = await _context.FantasticBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fantasticBooks == null)
            {
                return NotFound();
            }

            return View(fantasticBooks);
        }

        // GET: FantasticBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FantasticBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookName,AuthorName,CreatedDateTime")] FantasticBooks fantasticBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fantasticBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fantasticBooks);
        }

        // GET: FantasticBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FantasticBooks == null)
            {
                return NotFound();
            }

            var fantasticBooks = await _context.FantasticBooks.FindAsync(id);
            if (fantasticBooks == null)
            {
                return NotFound();
            }
            return View(fantasticBooks);
        }

        // POST: FantasticBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,AuthorName,CreatedDateTime")] FantasticBooks fantasticBooks)
        {
            if (id != fantasticBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fantasticBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FantasticBooksExists(fantasticBooks.Id))
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
            return View(fantasticBooks);
        }

        // GET: FantasticBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FantasticBooks == null)
            {
                return NotFound();
            }

            var fantasticBooks = await _context.FantasticBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fantasticBooks == null)
            {
                return NotFound();
            }

            return View(fantasticBooks);
        }

        // POST: FantasticBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FantasticBooks == null)
            {
                return Problem("Entity set 'ApplicationDbCon.FantasticBooks'  is null.");
            }
            var fantasticBooks = await _context.FantasticBooks.FindAsync(id);
            if (fantasticBooks != null)
            {
                _context.FantasticBooks.Remove(fantasticBooks);
            }
            
            await _context.SaveChangesAsync();
            TempData["keyMessage3"] = "Book deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool FantasticBooksExists(int id)
        {
          return (_context.FantasticBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
