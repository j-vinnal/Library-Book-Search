using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public CreateModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty] public Review Review { get; set; } = new Review();


        public SelectList BookSelectList { get; set; } = default!;
        public SelectList? ParentCommentSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            BookSelectList = new SelectList(await _context.Books.ToListAsync(), "Id", "Title");
            ParentCommentSelectList = new SelectList(await _context.Reviews.ToListAsync(), "Id", "Comment");

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
       
                return Page();
            }


            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}