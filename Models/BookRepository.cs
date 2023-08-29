using VektorelProje.Utility;

namespace VektorelProje.Models
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly VektorelDbContext _VektorelDbContext;
        public BookRepository(VektorelDbContext context) : base(context)
        {
            _VektorelDbContext = context;
        }

        public void Save()
        {
            _VektorelDbContext.SaveChanges();
        }

        public void Update(Book book)
        {
            _VektorelDbContext.Update(book);
        }
    }
}
