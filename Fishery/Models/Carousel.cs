using System;
using System.Collections.Generic;

namespace Fishery.Models
{
    public partial class Carousel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Src { get; set; }
        public string Alt { get; set; }
        public int Seq { get; set; }
    }
}
