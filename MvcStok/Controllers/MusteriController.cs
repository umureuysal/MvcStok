using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities dbStokEntities = new MvcDbStokEntities();
        // GET: Musteri
        public ActionResult List(string parametre)
        {
            var degerler = from musteri in dbStokEntities.Musteriler select musteri;
            if (!string.IsNullOrEmpty(parametre))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(parametre));
            }
            
            return View(degerler.ToList()) ;
            //var degerler = dbStokEntities.Musteriler.ToList();
            //return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(Musteriler parameter)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            dbStokEntities.Musteriler.Add(parameter);
            dbStokEntities.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var musteri = dbStokEntities.Musteriler.Find(id);
            dbStokEntities.Musteriler.Remove(musteri);
            dbStokEntities.SaveChanges();
            return RedirectToAction("List");
        }
        public ViewResult MusteriGuncelle(int id)
        {
            var musteri = dbStokEntities.Musteriler.Find(id);
            return View("MusteriGuncelle",musteri);
        }
        public ActionResult Guncelle(Musteriler musteri)
        {
            var mus = dbStokEntities.Musteriler.Find(musteri.MusteriID);
            mus.MusteriAd=musteri.MusteriAd;
            mus.MusteriSoyad = musteri.MusteriSoyad;
            dbStokEntities.SaveChanges();
            return RedirectToAction("List");
        }
    }
}