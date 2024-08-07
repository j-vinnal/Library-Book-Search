using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.BookAuthors
{
    public class IndexModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public IndexModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public IList<BookAuthor> BookAuthor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            BookAuthor = await _context.BookAuthors
                .Include(b => b.Author)
                .Include(b => b.Book).ToListAsync();
        }
    }
}
