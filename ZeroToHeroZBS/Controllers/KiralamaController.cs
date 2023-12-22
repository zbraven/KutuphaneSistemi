using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZeroToHeroZBS.Models;
using ZeroToHeroZBS.Utility;

namespace ZeroToHeroZBS.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admnin)]

    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _kiralamaRepository;   
        private readonly IKitapRepository _kitapRepository;
        private readonly IWebHostEnvironment _webHostEnvironment; //.Net'in bir sınıfı verilerle ilgili durumlarda kullanıyorum yardımcı olarak.

        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepository= kiralamaRepository;
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();
            return View(objKiralamaList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                       .Select(k => new SelectListItem
                       {
                           Text = k.KitapAdi,
                           Value = k.Id.ToString(),
                       }
                       );
            ViewBag.KitapList = KitapList;


            if (id == null || id == 0)
            {
                //ekleme
                return View();
            }
            else
            {
                //güncelleme
                Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
                if (kiralamaVt == null)
                {
                    return NotFound();
                }
                return View(kiralamaVt);
            }
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {
            if (ModelState.IsValid)
            {
                if (kiralama.Id == 0)
                {
                    _kiralamaRepository.Ekle(kiralama);
                    TempData["basarili"] = "Yeni Kitap Başarıyla Oluşturuldu.";
                }
                else
                {
                    _kiralamaRepository.Guncelle(kiralama);
                    TempData["basarili"] = "Kitap Güncelleme Başarılı.";
                }

                _kiralamaRepository.Kaydet();
                return RedirectToAction("Index", "Kiralama");

            }
            return View();
        }


        public IActionResult Sil(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                     .Select(k => new SelectListItem
                     {
                         Text = k.KitapAdi,
                         Value = k.Id.ToString(),
                     }
                     );
            ViewBag.KitapList = KitapList;

            if (id == null || id == 0)
            {
                return NotFound();

            }
            Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralamaVt == null)
            {
                return NotFound();
            }
            return View(kiralamaVt);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralama == null)
            {
                return NotFound();
            }
            _kiralamaRepository.Sil(kiralama);
            _kiralamaRepository.Kaydet();
            TempData["basarili"] = "Kayıt Silme İşlemi Başarılı.";
            return RedirectToAction("Index", "Kiralama");

        }

    }
}
