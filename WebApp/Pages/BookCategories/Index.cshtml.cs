using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.BookCategories
{
    public class IndexModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public IndexModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public IList<BookCategory> BookCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            BookCategory = await _context.BookCategories
                .Include(b => b.Book)
                .Include(b => b.Category).ToListAsync();
        }
    }
}
