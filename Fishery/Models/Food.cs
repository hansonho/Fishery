﻿using System;
using System.Collections.Generic;

namespace Fishery.Models
{
    public partial class Food
    {
        public Food()
        {
            FoodMenu = new HashSet<FoodMenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Desc { get; set; }
        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public string Pic3 { get; set; }
        public string OpenTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Seq { get; set; }

        public virtual ICollection<FoodMenu> FoodMenu { get; set; }
    }
}
