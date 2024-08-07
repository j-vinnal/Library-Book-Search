using Domain;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public DetailsModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public BookDto BookDto { get; set; } = default!;

        [BindProperty]
        public int ReviewRating { get; set; }

        [BindProperty]
        public string ReviewComment { get; set; } = default!;

        [BindProperty]
        public Guid ReviewId { get; set; } = default!;

        [BindProperty]
        public Guid BookId { get; set; } = default!;

        public async Task<IActionResult> OnPostAddReviewAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var newReview = new Review
            {
                Rating = ReviewRating,
                Comment = ReviewComment,
                BookId = BookId,
            };

            _context.Reviews.Add(newReview);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Books/Details", new { id = BookId });
        }

        public async Task<IActionResult> OnPostAddReplyAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var newReply = new Review
            {
                Comment = ReviewComment,
                ParentCommentId = ReviewId,
                BookId = BookId
            };

            _context.Reviews.Add(newReply);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Books/Details", new { id = BookId });
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.BookAuthors)!
                .ThenInclude(ba => ba.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)!
                .ThenInclude(bc => bc.Category)
                .Include(b => b.Reviews)!
                .ThenInclude(r => r.SubComments)
                .Where(b => b.Id == id)
                .Select(b => new BookDto
                {
                    Book = b,
                    AvgRating = b.Reviews!.Average(r => r.Rating),
                    RatingCount = b.Reviews!.Count(r => r.Rating != null),
                    ReviewCount = b.Reviews!.Count(r => r.ParentCommentId == null)
                })
                .FirstOrDefaultAsync();



            if (book == null)
            {
                return NotFound();
            }
            else
            {
                book.Book.Reviews = book.Book.Reviews?.Where(r => r.ParentCommentId == null).ToList();
                BookDto = book;
            }
            return Page();
        }


    }
}
