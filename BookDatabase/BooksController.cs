using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BooksController : Controller
{
    private readonly BookDbContext _context;

    public BooksController(BookDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.ToListAsync();
        return View(books);
    }

    // Implement Create, Edit, Delete actions
}
