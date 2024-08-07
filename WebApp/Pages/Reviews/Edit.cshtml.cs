using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Reviews
{
    public class EditModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public EditModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Review Review { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review =  await _context.Reviews.FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            Review = review;
           ViewData["BookId"] = new SelectList(_context.Books, "Id", "ISBN");
           ViewData["ParentCommentId"] = new SelectList(_context.Reviews, "Id", "Comment");
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

            _context.Attach(Review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(Review.Id))
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

        private bool ReviewExists(Guid id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
