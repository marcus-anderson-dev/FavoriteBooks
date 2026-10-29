using Microsoft.EntityFrameworkCore;
using FavoriteBooks.Models;

namespace FavoriteBooks.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Favorites {get; set;} = null!;
}