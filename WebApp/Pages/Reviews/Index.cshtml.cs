using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public IndexModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public IList<Review> Review { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Review = await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.ParentComment).ToListAsync();
        }
    }
}
