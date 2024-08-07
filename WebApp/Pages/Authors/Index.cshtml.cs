using Domain;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly DAL.EF.AppDbContext _context;

        public IndexModel(DAL.EF.AppDbContext context)
        {
            _context = context;
        }

        public IList<Author> Author { get; set; } = default!;
        public IList<AuthorDto> Authors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Authors = await _context.Authors
                .Select(a => new AuthorDto()
                {
                    Author = a,
                    BookCount = a.BookAuthors!.Count()
                })
                .ToListAsync();
        }
    }
}