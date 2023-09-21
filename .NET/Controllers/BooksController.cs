using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProfileMatching.Models;

namespace ProfileMatching.Controllers
{
    public class BooksController : Controller
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Books.Include(b => b.Author);
            return View(await dataContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,AuthorId,Title,PublicationYear")] Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName");
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,AuthorId,Title,PublicationYear")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookId))
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

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName");
            return View(book);
        }

        

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'DataContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: Books/Search
        [HttpPost]
        public async Task<IActionResult> Search(int? publicationYear)
        {
            if (publicationYear == null)
            {
                // If no publication year is provided, return all books.
                var allBooks = await _context.Books.Include(b => b.Author).ToListAsync();
                return View("SearchResults", allBooks);
            }

            // Search for books with the provided publication year.
            var books = await _context.Books
                .Where(b => b.PublicationYear.Year == publicationYear)
                .Include(b => b.Author)
                .ToListAsync();

            return View("SearchResults", books);
        }

        // GET: Books/SearchAuthor
        public IActionResult SearchAuthor()
        {
            return View();
        }

        // POST: Books/SearchAuthor
        [HttpPost]
        public async Task<IActionResult> SearchAuthor(string authorName)
        {
            if (string.IsNullOrEmpty(authorName))
            {
                // If no author name is provided, return all books.
                var allBooks = await _context.Books.Include(b => b.Author).ToListAsync();
                return View("SearchAuthorResults", allBooks);
            }

            // Search for books by the provided author name.
            var booksByAuthor = await _context.Books
                .Where(b => b.Author.AuthorName.Contains(authorName))
                .Include(b => b.Author)
                .ToListAsync();

            return View("SearchAuthorResults", booksByAuthor);
        }


        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
