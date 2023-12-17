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
            _uygulamaDbContext.Kitaplar.Include(k => k.KitapTuru).Include(k => k.KitapTuruId);
        }

        public void Ekle(T entity)
        {
           dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
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

        public IEnumerable<T> GetAll(string? includeProps= null)
        {
            IQueryable<T> sorgu = dbSet;

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu=sorgu.Include(includeProp);   
                }
            }
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
