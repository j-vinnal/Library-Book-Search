using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.EF;

public class AppDbContextFactory: IDesignTimeDbContextFactory<AppDbContext>
{
   
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("DataSource=C:/Projects/Uni/icd0008-23f/BookSearch/books.db;Cache=Shared");

        return new AppDbContext(optionsBuilder.Options);
    }
    
}