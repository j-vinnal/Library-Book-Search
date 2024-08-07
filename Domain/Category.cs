using System.ComponentModel.DataAnnotations;
using Domain.Database;

namespace Domain;

public class Category: BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    
    public DateTime CreatedAtDt { get; set; } = DateTime.Now;
    public DateTime UpdatedAtDt { get; set; } = DateTime.Now;

    public ICollection<Category>? Subcategories { get; set; }
    public ICollection<BookCategory>? BookCategories { get; set; }
    
    
}