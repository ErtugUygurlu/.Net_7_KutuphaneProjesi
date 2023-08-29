using VektorelProje.Models;

namespace VektorelProje.Models
{
    public interface IHireRepository : IRepository<Hire>
    {
        void Update(Hire Hire);
        void Save();
    }
}
