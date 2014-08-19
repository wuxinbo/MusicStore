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
    public class StoreMannagerController : MusicStoreController
    {
        //private MusicStoreDb db = new MusicStoreDb();

        //
        // GET: /StoreMannager/

        public ActionResult Index(String zhuanji_val, int GenreId = 0, int page = 1)
        {  
           
            //要跳过的记录数。
            int skip_count = (page-1)*page_size;
            //专辑名
            String genre_name=null;
            //得到记录数，默认为0。
            int count = 0;
            //全部查询所有的专辑，以及和其相关的实体。
            IEnumerable<Album> album = db.album.Include(a => a.Genre).
                                        Include(a => a.Artist);
            IEnumerable<Album> albums = null;
            // 如果GenreId不为0，表示用户选择了一个条件。根据传入的风格Id找到相应的名称
            if(GenreId !=0){
                 genre_name = (from g in db.genre
                                  where g.GenreId == GenreId
                                  select g.Name).Single();
            }
            //判断风格名称和专辑名（不为空且不是空白）
            if (genre_name != null && !String.IsNullOrEmpty(zhuanji_val) )
            {   //根据条件查询专辑名称。
                albums = album.
                Where(a => a.Genre.Name == genre_name && a.Title == zhuanji_val).
                OrderBy(a => a.Price).
                Skip(skip_count).Take(page_size);
                //得到记录数。
                count = album.
               Where(a => a.Genre.Name == genre_name && a.Title == zhuanji_val).
               Count();
            }
            else if (genre_name != null && String.IsNullOrEmpty(zhuanji_val))
            {
                 albums = album.
                  Where(a => a.Genre.Name == genre_name).
                  OrderBy(a => a.Price).
                  Skip(skip_count).Take(page_size);

                count = album.
               Where(a => a.Genre.Name == genre_name).
               Count();
            }
            else {
                //当没有条件时，默认采用这种查询。
                 albums = album.
                     
                     OrderBy(a => a.Price).
                     Skip(skip_count).Take(page_size);

                count = album.
                        Count();
            }

            genreSelectDownLIst();
            //返回查到的商品总数。
            InitPage(page, page_size, count);
            return View(albums.ToList());
        }

        

        


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
            genreSelectDownLIst();
            GenreAritistSelectDownLIst();
            return View();
        }

       

       

        /// <summary>
        /// 此方法接收从页面发来的post请求，将对象数据添加进数据库。
        /// </summary>
        /// <param name="album"> 从页面传过来的数据</param>
        /// <returns>
        /// 验证失败，返回上一页面，并保留数据。
        /// </returns>

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.album.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            genreSelectDownLIst();
            GenreAritistSelectDownLIst();
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
            genreSelectDownLIst();
            GenreAritistSelectDownLIst();
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
            genreSelectDownLIst();
            GenreAritistSelectDownLIst();
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