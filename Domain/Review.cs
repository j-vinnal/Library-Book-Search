using Domain.Database;

namespace Domain;

public class Review: BaseEntity
{
    public string Comment { get; set; } = default!;
    
    public int? Rating { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    public Review? ParentComment{ get; set; }
    
    public Guid BookId { get; set; }
    public Book? Book { get; set; }
    
    public DateTime CreatedAtDt { get; set; } = DateTime.Now;
    public DateTime UpdatedAtDt { get; set; } = DateTime.Now;
    
    public ICollection<Review>? SubComments { get; set; }

}