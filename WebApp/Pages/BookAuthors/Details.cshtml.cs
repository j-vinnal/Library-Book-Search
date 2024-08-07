using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.BookAuthors
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public DetailsModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public BookAuthor BookAuthor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookauthor = await _context.BookAuthors.FirstOrDefaultAsync(m => m.BookId == id);
            if (bookauthor == null)
            {
                return NotFound();
            }
            else
            {
                BookAuthor = bookauthor;
            }
            return Page();
        }
    }
}
