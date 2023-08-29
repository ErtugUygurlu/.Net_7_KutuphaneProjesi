using VektorelProje.Models;

namespace VektorelProje.Models
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book bookType);
        void Save();
    }
}
