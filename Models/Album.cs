using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Album
    {
        [ScaffoldColumn(false)]
        public int AlbumId { get; set; }

        [DisplayName("流派")]
        public int GenreId { get; set; }

        [DisplayName("艺术家")]
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "唱片名是必须的")]
        [StringLength(160)]
        public string Title { get; set; }

        [Required(ErrorMessage = "价格不能为空的！")]
        [Range(0.01, 100.00,
            ErrorMessage = "Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }

        [DisplayName("Album Art URL")]
        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}