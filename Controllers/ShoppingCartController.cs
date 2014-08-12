using MusicStore.Models;
using MusicStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/
        private MusicStoreDb store = new MusicStoreDb();
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            // Set up our ViewModel
            var viewModel = new ShoppingViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        [Authorize]
        public ActionResult AddToCart(int id,int count)
        {
            // 从数据库中得到要加入购物车的专辑商品。
            var addedAlbum = store.album
            .Single(album => album.AlbumId == id);
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedAlbum);
            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            // 得到需要删除的专辑的标题。
            string albumName = store.Carts
            .Single(item => item.RecordId == id).Album.Title;
            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);
            // Display the confirmation message
            var results = new ShoppingRemoveModel
            {
                Message = Server.HtmlEncode(albumName) +
                " 已经从你的购物车中移除！.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewBag.CartCount = cart.GetCount();
            return PartialView("CartSummary");
        }

    }
}
