using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MusicStore.Models;
namespace MusicStore.Controllers
{
    public class HomeController : MusicStoreController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Search(String key,int page =1)
        {
            //要跳过的记录数。
            int skip_count = (page - 1) * page_size;
            //根据关键字进行模糊查询。
            var album = db.album.Include(a => a.Genre);
            IEnumerable<Album> albums = null;
            if (!String.IsNullOrEmpty(key))
            {
                albums = album.
                      Where(a => a.Title.Contains(key)).
                      OrderBy(a => a.Price).Skip(skip_count).Take(page_size);
            }
            else {
                albums = album;
                      
            }
           //得到商品数目。
            int count = album.
                      Where(a => a.Title.Contains(key)).Count();
            ViewBag.key = key;
            InitPage(page, page_size, count);
            return View(albums);
        }
        
       
       

      
        
    }
}
