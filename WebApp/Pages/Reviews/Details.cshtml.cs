using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Reviews
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public DetailsModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public Review Review { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            else
            {
                Review = review;
            }
            return Page();
        }
    }
}
