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
    public class UrunController : Controller
    {
        MvcDbStokEntities dbStokEntities = new MvcDbStokEntities();
        // GET: Urun
        public ActionResult List(int page=1)
        {
            //var degerler = dbStokEntities.Urunler.ToList();
            var degerler = dbStokEntities.Urunler.ToList().ToPagedList(page, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in dbStokEntities.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr=degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urunler parameter)
        {
            var kategori = dbStokEntities.Kategoriler.Where(p=>p.KategoriID==parameter.Kategoriler.KategoriID).FirstOrDefault();
            parameter.Kategoriler = kategori;
            dbStokEntities.Urunler.Add(parameter);
            dbStokEntities.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Sil(int id)
        {
            var urun=dbStokEntities.Urunler.Find(id);
            dbStokEntities.Urunler.Remove(urun);
            dbStokEntities.SaveChanges();
            return RedirectToAction("List");
        }
        public ViewResult UrunGuncelle(int id)
        {
            var urun = dbStokEntities.Urunler.Find(id);
            List<SelectListItem> degerler = (from i in dbStokEntities.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGuncelle",urun);
        }
        public ActionResult Guncelle(Urunler u)
        {
            var urun = dbStokEntities.Urunler.Find(u.UrunID);
            urun.UrunAd = u.UrunAd;
            urun.UrunID = u.UrunID;
            urun.Stok = u.Stok;
            urun.Marka=u.Marka;
            urun.UrunKategori=u.Kategoriler.KategoriID;
            urun.UrunFiyat=u.UrunFiyat;
            dbStokEntities.SaveChanges();
            return RedirectToAction("List");
        }
    }
}