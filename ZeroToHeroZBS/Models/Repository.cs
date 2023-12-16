using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ZeroToHeroZBS.Utility;

namespace ZeroToHeroZBS.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        internal DbSet<T> dbSet;  // dbSet=_uygulamaDbContext.KitapTurleri
        public Repository(UygulamaDbContext uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
            this.dbSet = _uygulamaDbContext.Set<T>();   
        }

        public void Ekle(T entity)
        {
           dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu=sorgu.Where(filter);
            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> sorgu = dbSet;
            return sorgu.ToList();
        }

        public void Sil(T Entity)
        {
            dbSet.Remove(Entity);
        }

        public void SilAralik(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);    
        }
    }
}
