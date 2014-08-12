using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MusicStore.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        [DisplayName("专辑")]
        public string Name { get; set; }
    }
}
