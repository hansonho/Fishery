using System;
using System.Collections.Generic;

namespace Fishery.Models
{
    public partial class FoodMenu
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public string Pic3 { get; set; }
        public string Desc { get; set; }

        public virtual Food Food { get; set; }
    }
}
