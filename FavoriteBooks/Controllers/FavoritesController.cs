using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FavoriteBooks.Data;
using FavoriteBooks.Models;

namespace FavoriteBooks.Controllers;

[ApiController]
[Route("api/favorites")]
public class FavoritesController : ControllerBase
{
    private readonly AppDbContext _db;

    public FavoritesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<List<Book>> GetAll()
    {
        return await _db.Favorites.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Add(Book book)
    {
        bool alreadySaved = await _db.Favorites.AnyAsync(b => b.Key == book.Key);
        if (alreadySaved)
        {
            return Conflict(book.Title + " is already in your favorites.");
        }

        book.Id = 0;
        _db.Favorites.Add(book);
        await _db.SaveChangesAsync();

        return Ok(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var book = await _db.Favorites.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _db.Favorites.Remove(book);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}