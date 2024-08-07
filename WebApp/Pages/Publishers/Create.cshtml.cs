using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Publishers
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
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Publishers.Add(Publisher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
