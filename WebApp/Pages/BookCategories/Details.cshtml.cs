using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.BookCategories
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public DetailsModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public BookCategory BookCategory { get; set; } = default!;
        [BindProperty (SupportsGet = true)]
        public Guid BookId { get; set; }
        [BindProperty (SupportsGet = true)]
        public Guid CategoryId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
          

            var bookcategory = await _context.BookCategories.FirstOrDefaultAsync(m => m.BookId == BookId && m.CategoryId == CategoryId);
            if (bookcategory == null)
            {
                return NotFound();
            }
            else
            {
                BookCategory = bookcategory;
            }
            return Page();
        }
    }
}
