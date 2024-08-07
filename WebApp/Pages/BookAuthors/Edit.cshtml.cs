using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.BookAuthors
{
    public class EditModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public EditModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookAuthor BookAuthor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? bookId, Guid? authorId)
        {
            if (bookId == null || authorId == null)
            {
                return NotFound();
            }

            var bookauthor =  await _context.BookAuthors.FirstOrDefaultAsync(m => m.BookId == bookId && m.AuthorId == authorId);
            if (bookauthor == null)
            {
                return NotFound();
            }
            BookAuthor = bookauthor;
           ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name");
           ViewData["BookId"] = new SelectList(_context.Books, "Id", "ISBN");
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

            _context.Attach(BookAuthor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookAuthorExists(BookAuthor.BookId))
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

        private bool BookAuthorExists(Guid id)
        {
            return _context.BookAuthors.Any(e => e.BookId == id);
        }
    }
}
