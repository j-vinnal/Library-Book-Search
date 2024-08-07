namespace Domain.Dto;

public class BookDto
{
    public Book Book { get; set; } = default!;
    public double? AvgRating { get; set; }
    public int? RatingCount { get; set; }
    public int? ReviewCount { get; set; }

}