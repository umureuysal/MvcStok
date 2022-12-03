using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        MvcDbStokEntities dbStokEntities = new MvcDbStokEntities();
        // GET: Kategori
        public ActionResult List(int sayfa=1)
        {
            //var degerler = dbStokEntities.Kategoriler.ToList();
            var degerler = dbStokEntities.Kategoriler.ToList().ToPagedList(sayfa , 4);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
           return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(Kategoriler parameter1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            dbStokEntities.Kategoriler.Add(parameter1);
            dbStokEntities.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var kategori = dbStokEntities.Kategoriler.Find(id);
            dbStokEntities.Kategoriler.Remove(kategori);
            dbStokEntities.SaveChanges();
            return RedirectToAction("List");
        }
        public ViewResult KategoriGuncelle(int id)
        {
            var kategori = dbStokEntities.Kategoriler.Find(id);
            return View("KategoriGuncelle", kategori);
        }
        public ActionResult Guncelle(Kategoriler kategori)
        {
            var ktg = dbStokEntities.Kategoriler.Find(kategori.KategoriID);
            ktg.KategoriAd = kategori.KategoriAd;
            dbStokEntities.SaveChanges();
            return RedirectToAction("List");
        }
            
    }
}