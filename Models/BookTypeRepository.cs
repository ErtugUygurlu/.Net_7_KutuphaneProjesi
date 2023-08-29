
using VektorelProje.Utility;

namespace VektorelProje.Models
{
    public class BookTypeRepository : Repository<BookType>, IBookTypeRepository
    {
        private readonly VektorelDbContext _VektorelDbContext;
        public BookTypeRepository(VektorelDbContext context) : base(context)
        {
            _VektorelDbContext = context;
        }

        public void Save()
        {
            _VektorelDbContext.SaveChanges();
        }

        public void Update(BookType bookType)
        {
            _VektorelDbContext.Update(bookType);
        }
    }
}
