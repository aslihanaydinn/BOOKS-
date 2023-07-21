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
    public class PoemBooksController : Controller
    {
        private readonly ApplicationDbCon _context;

        public PoemBooksController(ApplicationDbCon context)
        {
            _context = context;
        }

        // GET: PoemBooks
        public async Task<IActionResult> Index()
        {
              return _context.PoemBooks != null ? 
                          View(await _context.PoemBooks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbCon.PoemBooks'  is null.");
        }

        // GET: PoemBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PoemBooks == null)
            {
                return NotFound();
            }

            var poemBooks = await _context.PoemBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poemBooks == null)
            {
                return NotFound();
            }

            return View(poemBooks);
        }

        // GET: PoemBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PoemBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookName,AuthorName,CreatedDateTime")] PoemBooks poemBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poemBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poemBooks);
        }

        // GET: PoemBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PoemBooks == null)
            {
                return NotFound();
            }

            var poemBooks = await _context.PoemBooks.FindAsync(id);
            if (poemBooks == null)
            {
                return NotFound();
            }
            return View(poemBooks);
        }

        // POST: PoemBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,AuthorName,CreatedDateTime")] PoemBooks poemBooks)
        {
            if (id != poemBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poemBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoemBooksExists(poemBooks.Id))
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
            return View(poemBooks);
        }

        // GET: PoemBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PoemBooks == null)
            {
                return NotFound();
            }

            var poemBooks = await _context.PoemBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poemBooks == null)
            {
                return NotFound();
            }

            return View(poemBooks);
        }

        // POST: PoemBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PoemBooks == null)
            {
                return Problem("Entity set 'ApplicationDbCon.PoemBooks'  is null.");
            }
            var poemBooks = await _context.PoemBooks.FindAsync(id);
            if (poemBooks != null)
            {
                _context.PoemBooks.Remove(poemBooks);
            }
            
            await _context.SaveChangesAsync();
            TempData["keyMessage4"] = "Book deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool PoemBooksExists(int id)
        {
          return (_context.PoemBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
