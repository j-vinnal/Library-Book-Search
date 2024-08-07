using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly DAL.EF.AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IList<BookDto> BookDto { get; set; } = default!;

    [BindProperty(SupportsGet = true)] public string? Search { get; set; } = "";
    [BindProperty(SupportsGet = true)] public bool InAuthor { get; set; }

    [BindProperty(SupportsGet = true)] public bool Reset { get; set; }
    [BindProperty(SupportsGet = true)] public bool InPublisher { get; set; }
    [BindProperty(SupportsGet = true)] public bool InTitle { get; set; }
    [BindProperty(SupportsGet = true)] public bool InSummary { get; set; }
    [BindProperty(SupportsGet = true)] public bool InComment { get; set; }
    [BindProperty(SupportsGet = true)] public bool InGenre { get; set; }


    public IndexModel(ILogger<IndexModel> logger, DAL.EF.AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {

        var query = _context.Books
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors)!
            .ThenInclude(ba => ba.Author)
            .Include(b => b.Reviews)!
            .Include(b => b.BookCategories)!
            .ThenInclude(ba => ba.Category)
            .Select(b => new BookDto
            {
                Book = b,
                AvgRating = b.Reviews!.Average(r => r.Rating),
                RatingCount = b.Reviews!.Count(r => r.Rating != null),
                ReviewCount = b.Reviews!.Count(r => r.ParentCommentId == null)
            })
            .OrderByDescending(b => b.AvgRating)
            .AsQueryable();
        

        if (!string.IsNullOrEmpty(Search))
        {
            Search = Search.ToUpper().Trim();

            if (Reset)
            {
                InAuthor = false;
                InPublisher = false;
                InTitle = false;
                InSummary = false;
                InComment = false;
                InGenre = false;
                Search = "";
            }

            if (!(InTitle || InSummary || InPublisher || InAuthor || InComment || InGenre))
            {

                query = query.Where(b =>
                    b.Book.Title.ToUpper().Contains(Search) ||
                    b.Book.Summary.ToUpper().Contains(Search) ||
                    b.Book.Publisher!.Name.ToUpper().Contains(Search) ||
                    b.Book.BookAuthors!.Any(ba =>
                        ba.Author!.FirstName.ToUpper().Contains(Search) ||
                        ba.Author!.LastName.ToUpper().Contains(Search)) ||
                    b.Book.Reviews!.Any(c => c.Comment.ToUpper().Contains(Search)));
            }
            else
            {
                if (InTitle)
                {
                    query = query.Where(b => b.Book.Title.ToUpper().Contains(Search));
                }

                if (InSummary)
                {
                    query = query.Where(b => b.Book.Summary.ToUpper().Contains(Search));
                }

                if (InPublisher)
                {
                    query = query.Where(b => b.Book.Publisher!.Name.ToUpper().Contains(Search));
                }

                if (InAuthor)
                {
                    string[] searchParts = Search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    query = query.Where(b => b.Book.BookAuthors!.Any(ba =>
                        searchParts.All(part =>
                            ba.Author!.FirstName.ToUpper().Contains(part.ToUpper()) ||
                            ba.Author!.LastName.ToUpper().Contains(part.ToUpper())
                        )
                    ));
                }

                if (InComment)
                {
                    query = query.Where(b => b.Book.Reviews!.Any(r => r.Comment.ToUpper().Contains(Search)));
                }


                if (InGenre)
                {
                    query = query.Where(b => b.Book.BookCategories!.Any(c => c.Category!.Name.ToUpper().Contains(Search)));
                }
            }
        }

        BookDto = await query.ToListAsync();

        return Page();
    }
}