using System.ComponentModel.DataAnnotations;
using Domain.Database;

namespace Domain;

public class Publisher: BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    public DateTime CreatedAtDt { get; set; } = DateTime.Now;
    public DateTime UpdatedAtDt { get; set; } = DateTime.Now;
    
    public ICollection<Book>? Books { get; set; }
}