using System.Linq.Expressions;

namespace ZeroToHeroZBS.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProps = null);    //Hepsini getir
        T Get(Expression<Func<T, bool>> filter, string? includeProps = null);      //ID'si şu olanı getir, filtrele
        void Ekle(T entity); //Ekle
        void Sil(T entity); //Sil
        void SilAralik(IEnumerable<T> entities); //Belli bir aralıktaki verileri silmek için
    }
}
