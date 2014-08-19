using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{   
    public class MusicStoreController : Controller
    {
        //
        // GET: /MusicStore/
        protected MusicStoreDb db = new MusicStoreDb();
        
        //默认一页只显示10条记录。
        protected const int page_size = 10;


       /// <summary>
       /// 对得到的记录数进行格式化。
       /// </summary>
       /// <param name="page_size"> 一页显示的默认记录数</param>
       /// <param name="count"> 记录数</param>
       /// <returns>
       /// 格式化之后的页数
       /// </returns>
        protected static int formatPage(int page_size, int count)
        {
            return count % page_size == 0 ? count / page_size : (count / page_size) + 1;
        }
        /// <summary>
        /// 对得到的页数和记录数赋值给ViewBag。传递到前端
        /// </summary>
        /// <param name="page"> 第几页</param>
        /// <param name="page_size">默认记录数</param>
        /// <param name="count">总的记录数</param>
        protected void InitPage(int page, int page_size, int count)
        {   //得到记录数
            ViewBag.counts = count;

            //得到格式化之后的页数。
            ViewBag.count = formatPage(page_size, count);
            //当前页面
            ViewBag.current_page = page;
        }
        /// <summary>
        /// 生成风格下拉列表。
        /// </summary>
        protected void genreSelectDownLIst()
        {
            ViewBag.GenreId = new SelectList(db.genre, "GenreId", "Name");
        }
        /// <summary>
        /// 生成艺术家下拉列表。
        /// </summary>
        protected void GenreAritistSelectDownLIst()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
        }
    }
}
