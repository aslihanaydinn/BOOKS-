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
    public class DystopianBooksController : Controller
    {
        private readonly ApplicationDbCon _context;

        public DystopianBooksController(ApplicationDbCon context)
        {
            _context = context;
        }

        // GET: DystopianBooks
        public async Task<IActionResult> Index()
        {
              return _context.DystopianBooks != null ? 
                          View(await _context.DystopianBooks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbCon.DystopianBooks'  is null.");
        }

        // GET: DystopianBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DystopianBooks == null)
            {
                return NotFound();
            }

            var dystopianBooks = await _context.DystopianBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dystopianBooks == null)
            {
                return NotFound();
            }

            return View(dystopianBooks);
        }

        // GET: DystopianBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DystopianBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookName,AuthorName,CreatedDateTime")] DystopianBooks dystopianBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dystopianBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dystopianBooks);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DystopianBooks == null)
            {
                return NotFound();
            }

            var dystopianBooks = await _context.DystopianBooks.FindAsync(id);
            if (dystopianBooks == null)
            {
                return NotFound();
            }
            return View(dystopianBooks);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,AuthorName,CreatedDateTime")] DystopianBooks dystopianBooks)
        {
            if (id != dystopianBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dystopianBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DystopianBooksExists(dystopianBooks.Id))
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
            return View(dystopianBooks);
        }

        // GET: DystopianBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DystopianBooks == null)
            {
                return NotFound();
            }

            var dystopianBooks = await _context.DystopianBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dystopianBooks == null)
            {
                return NotFound();
            }

            return View(dystopianBooks);
        }

        // POST: DystopianBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DystopianBooks == null)
            {
                return Problem("Entity set 'ApplicationDbCon.DystopianBooks'  is null.");
            }
            var dystopianBooks = await _context.DystopianBooks.FindAsync(id);
            if (dystopianBooks != null)
            {
                _context.DystopianBooks.Remove(dystopianBooks);
            }
            
            await _context.SaveChangesAsync();
            TempData["keyMessage2"] = "Book deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool DystopianBooksExists(int id)
        {
          return (_context.DystopianBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
