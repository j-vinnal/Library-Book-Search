using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public IndexModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categories
                .Include(c => c.ParentCategory).ToListAsync();
        }
    }
}
