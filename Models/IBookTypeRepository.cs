using VektorelProje.Models;

namespace VektorelProje.Models
{
    public interface IBookTypeRepository: IRepository<BookType>
    {
        void Update(BookType bookType);
        void Save();
    }
}
