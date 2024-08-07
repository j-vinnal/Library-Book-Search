using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.BookCategories
{
    public class CreateModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public CreateModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title");
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public BookCategory BookCategory { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BookCategories.Add(BookCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
