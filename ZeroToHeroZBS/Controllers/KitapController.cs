using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZeroToHeroZBS.Models;
using ZeroToHeroZBS.Utility;

namespace ZeroToHeroZBS.Controllers
{
    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;   

        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();
            return View(objKitapList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
                       .Select(k => new SelectListItem
                       {
                           Text = k.Ad,
                           Value = k.Id.ToString(),
                       }
                       );
            ViewBag.KitapTuruList = KitapTuruList;


            if (id == null || id == 0)
            {
                //ekleme
                return View();
            }
            else
            {
                //güncelleme
                Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
                if (kitapVt == null)
                {
                    return NotFound();
                }
                return View(kitapVt);
            }
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kitap kitap,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath= Path.Combine(wwwRootPath, @"img");

                using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            kitap.ResimUrl = @"\img\" + file.FileName;

                _kitapRepository.Ekle(kitap);
                _kitapRepository.Kaydet();
                TempData["basarili"] = "Yeni Kitap Başarıyla Oluşturuldu.";
                return RedirectToAction("Index", "Kitap");

            }
            return View();
        }





        /* 
         public IActionResult Guncelle(int? id)
         {
             if (id == null || id == 0)
             {
                 return NotFound();

             }
             Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
             if (kitapVt == null)
             {
                 return NotFound();
             }
             return View(kitapVt);
         }

         [HttpPost]
         public IActionResult Guncelle(Kitap kitap)
         {
             if (ModelState.IsValid)
             {
                 _kitapRepository.Guncelle(kitap);
                 _kitapRepository.Kaydet();
                 TempData["basarili"] = "Yeni Kitap Başarıyla Güncellendi.";
                 return RedirectToAction("Index", "Kitap");

             }
             return View();
         }
        */

        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }
            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Kayıt Silme İşlemi Başarılı.";
            return RedirectToAction("Index", "Kitap");

        }

    }
}
