using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VektorelProje.Models;

namespace VektorelProje.Utility
{
    public class VektorelDbContext : IdentityDbContext
    {

        public VektorelDbContext(DbContextOptions<VektorelDbContext> options):base(options) { }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Hire> Hires { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }



    }
}
