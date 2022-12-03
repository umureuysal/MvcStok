using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        MvcDbStokEntities mvcDbStokEntities = new MvcDbStokEntities();
        // GET: Satis
        public ActionResult Index()
        {
            //var satis = mvcDbStokEntities.Urunler.Find(satislar.Urun);
            //return View(satis.UrunAd);
            return View();
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(Satislar satislar)
        {
            mvcDbStokEntities.Satislar.Add( satislar);
            mvcDbStokEntities.SaveChanges();
            return View("Index");
        }
    }
}