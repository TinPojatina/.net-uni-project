using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            ViewBag.Authors = new SelectList(authors, "AuthorId", "LastName");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Title,ISBN,PublicationYear,Price,IsAvailable,Genre,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.CreateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }

            var authors = await _authorService.GetAllAuthorsAsync();
            ViewBag.Authors = new SelectList(authors, "AuthorId", "LastName", book.AuthorId);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var authors = await _authorService.GetAllAuthorsAsync();
            ViewBag.Authors = new SelectList(authors, "AuthorId", "LastName", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,ISBN,PublicationYear,Price,IsAvailable,Genre,AuthorId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }

            var authors = await _authorService.GetAllAuthorsAsync();
            ViewBag.Authors = new SelectList(authors, "AuthorId", "LastName", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Additional action 1: Get available books
        public async Task<IActionResult> Available()
        {
            var books = await _bookService.GetAvailableBooksAsync();
            return View("Index", books);
        }

        // Additional action 2: Get books by genre
        public async Task<IActionResult> ByGenre(string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return RedirectToAction(nameof(Index));
            }

            var books = await _bookService.GetBooksByGenreAsync(genre);
            ViewBag.CurrentGenre = genre;
            return View("Index", books);
        }
    }
}