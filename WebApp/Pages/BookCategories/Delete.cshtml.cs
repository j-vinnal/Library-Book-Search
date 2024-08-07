using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.BookCategories
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public DeleteModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
           
            var bookcategory = await _context.BookCategories.Where(bc => bc.BookId == BookId && bc.CategoryId == CategoryId).FirstOrDefaultAsync();
            if (bookcategory != null)
            {
                BookCategory = bookcategory;
                _context.BookCategories.Remove(BookCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
