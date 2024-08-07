using System.ComponentModel.DataAnnotations;
using Domain.Database;

namespace Domain;

public class Book : BaseEntity
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;
    
    [MaxLength(128)]
    public string ISBN { get; set; } = default!;
    
    [MaxLength(128)]
    public string? ImageUrl { get; set; }
    
    public int PublicationYear { get; set; }
    public ELanguage Language { get; set; }
    
    public string Summary { get; set; } = default!;
    
    public Guid PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
    
    public DateTime CreatedAtDt { get; set; } = DateTime.Now;
    public DateTime UpdatedAtDt { get; set; } = DateTime.Now;
    
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<BookCategory>? BookCategories { get; set; }
    public ICollection<BookAuthor>? BookAuthors { get; set; }
}