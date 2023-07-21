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
    public class ClassicBooksController : Controller
    {
        private readonly ApplicationDbCon _context;

        public ClassicBooksController(ApplicationDbCon context)
        {
            _context = context;
        }

        // GET: ClassicBooks
        public async Task<IActionResult> Index()
        {
              return _context.ClassicBooks != null ? 
                          View(await _context.ClassicBooks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbCon.ClassicBooks'  is null.");
        }

        // GET: ClassicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassicBooks == null)
            {
                return NotFound();
            }

            var classicBooks = await _context.ClassicBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classicBooks == null)
            {
                return NotFound();
            }

            return View(classicBooks);
        }

        // GET: ClassicBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookName,AuthorName,CreatedDateTime")] ClassicBooks classicBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classicBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classicBooks);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassicBooks == null)
            {
                return NotFound();
            }

            var classicBooks = await _context.ClassicBooks.FindAsync(id);
            if (classicBooks == null)
            {
                return NotFound();
            }
            return View(classicBooks);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,AuthorName,CreatedDateTime")] ClassicBooks classicBooks)
        {
            if (id != classicBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classicBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassicBooksExists(classicBooks.Id))
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
            return View(classicBooks);
        }

        // GET: ClassicBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassicBooks == null)
            {
                return NotFound();
            }

            var classicBooks = await _context.ClassicBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classicBooks == null)
            {
                return NotFound();
            }

            return View(classicBooks);
        }

        // POST: ClassicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassicBooks == null)
            {
                return Problem("Entity set 'ApplicationDbCon.ClassicBooks'  is null.");
            }
            var classicBooks = await _context.ClassicBooks.FindAsync(id);
            if (classicBooks != null)
            {
                _context.ClassicBooks.Remove(classicBooks);
            }
            
            await _context.SaveChangesAsync();
            TempData["keyMessage1"] = "Book deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool ClassicBooksExists(int id)
        {
          return (_context.ClassicBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
