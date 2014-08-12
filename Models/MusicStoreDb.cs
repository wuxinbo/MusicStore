using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class MusicStoreDb:DbContext
    {
        public MusicStoreDb() : base("name=DefaultConnection") { }
        public DbSet<Album> album { get; set; }
        public DbSet<Genre> genre { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderDetail> orderdetail { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Artist> Artists { get; set; }
    }
}