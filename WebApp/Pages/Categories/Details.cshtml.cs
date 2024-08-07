using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public DetailsModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }
    }
}
