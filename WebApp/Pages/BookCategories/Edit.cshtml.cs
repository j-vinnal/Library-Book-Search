using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.BookCategories
{
    public class EditModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public EditModel(DAL.EF.AppDbContext context)
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
            
            var bookcategory =  await _context.BookCategories.FirstOrDefaultAsync(m => m.BookId == BookId && m.CategoryId == CategoryId);
            if (bookcategory == null)
            {
                return NotFound();
            }
            BookCategory = bookcategory;
            
            
           ViewData["BookId"] = new SelectList(_context.Books, "Id", "ISBN");
           ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BookCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookCategoryExists())
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookCategoryExists()
        {
            return _context.BookCategories.Any(m => m.BookId == BookId && m.CategoryId == CategoryId);
        }
    }
}
