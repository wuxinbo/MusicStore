using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MusicStore.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [DisplayName("风格")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Album> Albums { get; set; }
    }
}
