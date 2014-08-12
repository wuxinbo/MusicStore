using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class StoreMannagerController : Controller
    {
        private MusicStoreDb db = new MusicStoreDb();

        //
        // GET: /StoreMannager/

        public ActionResult Index(int page=1)
        {   //默认一页只显示10条记录。
            const int page_size = 10;
            //要跳过的记录数。
            int skip_count = (page-1)*page_size;
            var album = db.album.Include(a => a.Genre).
                Include(a => a.Artist).
                OrderBy(a=>a.Price).
                Skip(skip_count).Take(page_size);
            //得到记录数。
            int count=db.album.Include(a => a.Genre).
                Include(a => a.Artist).Count();
            //得到格式化之后的页数。
            ViewBag.count = count % page_size == 0 ? count / page_size : (count / page_size)+1;
            ViewBag.current_page = page;
            return View(album.ToList());
        }

        //
        // GET: /StoreMannager/Details/5

        public ActionResult Details(int id = 0)
        {
            Album album = db.album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // GET: /StoreMannager/Create

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.genre, "GenreId", "Name");
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            return View();
        }

        //
        // POST: /StoreMannager/Create

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.album.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.genre, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // GET: /StoreMannager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Album album = db.album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.genre, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // POST: /StoreMannager/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.genre, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // GET: /StoreMannager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Album album = db.album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // POST: /StoreMannager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.album.Find(id);
            db.album.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}