
using VektorelProje.Utility;

namespace VektorelProje.Models
{
    public class HireRepository : Repository<Hire>, IHireRepository
    {
        private readonly VektorelDbContext _VektorelDbContext;
        public HireRepository(VektorelDbContext context) : base(context)
        {
            _VektorelDbContext = context;
        }

        public void Save()
        {
            _VektorelDbContext.SaveChanges();
        }

        public void Update(Hire hire)
        {
            _VektorelDbContext.Update(hire);
        }
    }
}
