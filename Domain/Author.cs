using System.ComponentModel.DataAnnotations;
using Domain.Database;

namespace Domain;

public class Author : BaseEntity
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    public DateTime CreatedAtDt { get; set; } = DateTime.Now;
    public DateTime UpdatedAtDt { get; set; } = DateTime.Now;
    public ICollection<BookAuthor>? BookAuthors { get; set; }
}