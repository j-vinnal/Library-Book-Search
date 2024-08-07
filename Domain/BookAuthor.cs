using Microsoft.EntityFrameworkCore;

namespace Domain;

[PrimaryKey(nameof(BookId), nameof(AuthorId))]
public class BookAuthor
{
    public Guid BookId { get; set; }
    public Book? Book { get; set; }

    public Guid AuthorId { get; set; }
    public Author? Author { get; set; }

    public DateTime CreatedAtDt { get; set; } = DateTime.Now;
    public DateTime UpdatedAtDt { get; set; } = DateTime.Now;
}