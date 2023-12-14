using System.Linq.Expressions;

namespace ZeroToHeroZBS.Models
{
    public interface IKitapRepository : IRepository<Kitap>
    {
        void Guncelle(Kitap kitap);
        void Kaydet();
    }
}
