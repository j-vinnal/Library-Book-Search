using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class AppDbContext: DbContext
{
    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<Author> Authors { get; set; } = default!;
    public DbSet<BookAuthor> BookAuthors { get; set; } = default!;
    public DbSet<BookCategory> BookCategories { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    public DbSet<Publisher> Publishers { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}