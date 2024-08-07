namespace Domain.Dto;

public class AuthorDto
{
    public Author Author { get; set; } = default!;
    public int BookCount { get; set; }
}