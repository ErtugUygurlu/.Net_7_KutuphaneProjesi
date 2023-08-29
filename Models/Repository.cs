using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VektorelProje.Utility;

namespace VektorelProje.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VektorelDbContext _VektorelDbContext;
        internal DbSet<T> dbset;
        

        public Repository(VektorelDbContext context)
        {
            _VektorelDbContext = context;
            this.dbset=_VektorelDbContext.Set<T>();
            _VektorelDbContext.Books.Include(k=>k.bookType).Include(k=>k.BookTypeId);
        }

      

        public void Add(T entity)
        {
           dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> sorgu = dbset;
            sorgu=sorgu.Where(filter);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps=null)
        {
            IQueryable<T> sorgu = dbset;
            if(!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu=sorgu.Include(includeProp);
                }
            }
            return sorgu.ToList();
        }
    }
}
