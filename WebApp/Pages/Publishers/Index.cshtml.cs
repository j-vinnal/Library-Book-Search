using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public IndexModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Publisher = await _context.Publishers.ToListAsync();
        }
    }
}
