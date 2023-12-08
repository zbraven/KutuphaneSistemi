using System.ComponentModel.DataAnnotations;

namespace ZeroToHeroZBS.Models
{
    public class KitapTuru
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Ad { get; set; }
    }
}
