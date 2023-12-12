using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZeroToHeroZBS.Models
{
    public class KitapTuru
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Kitap Türü Adı Boş Bırakılamaz")] //boş geçilemez
        [MaxLength(25)] //25 karakteri geçmesin
        [DisplayName("Kitap Türü Adı")]
        public string Ad { get; set; }
    }
}
