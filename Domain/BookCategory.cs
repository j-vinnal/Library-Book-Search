using Microsoft.EntityFrameworkCore;

namespace Domain;

[PrimaryKey(nameof(BookId), nameof(CategoryId))]
public class BookCategory
{
    public Guid BookId { get; set; }
    public Book? Book { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public DateTime CreatedAtDt { get; set; } = DateTime.Now;
    public DateTime UpdatedAtDt { get; set; } = DateTime.Now;

}